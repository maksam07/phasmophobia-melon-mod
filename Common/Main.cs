using MelonLoader;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Console = System.Console;
using Object = UnityEngine.Object;
using String = System.String;

namespace C4PhasMod {
    public class Main : MelonMod {
        public override void OnApplicationStart() {
            BasicInject.Main();
            Debug.Msg("Set console title to: Phasmophobia", 1);
            Console.Title = string.Format("Phasmophobia MOD");

            HandleConfig();
        }

        public override void OnSceneWasLoaded(int index, string level) {
            if (initializedScene == 1) {
                Debug.Msg("Game Started...", 1);
                gameStarted = true;
            }
        }

        public override void OnSceneWasInitialized(int index, string level) {
            initializedScene = index;
            if (initializedScene > 1 && canRun) {
                canRun = false;
                coRoutine = null;
                isRunning = false;
                new Thread(() => {
                    while (initializedScene > 1) {
                        if (!isRunning) {
                            isRunning = true;
                            coRoutine = MelonCoroutines.Start(CollectGameObjects());
                        }
                        Thread.Sleep(5000);
                    }
                }).Start();
            }

            if (initializedScene == 1 && canRun) {
                canRun = false;
                coRoutine = null;
                isRunning = false;
                new Thread(() => {
                    while (initializedScene == 1) {
                        if (!isRunning) {
                            isRunning = true;
                            coRoutine = MelonCoroutines.Start(CollectPlayerObjects());
                        }
                        Thread.Sleep(5000);
                    }
                }).Start();
            }

            Debug.Msg("Initialized Scene: " + mapNames[initializedScene], 1);
            if (gameStarted && initializedScene == 1)
                DisableAll();

            if (initializedScene == 1 && !canRun) {
                MelonCoroutines.Stop(coRoutine);
                canRun = true;
            }
        }
        public override void OnUpdate() {
            Keyboard keyboard = Keyboard.current;

            if (keyboard.leftArrowKey.wasPressedThisFrame) {
                CheatToggles.enableBI = !CheatToggles.enableBI;
                if (CheatToggles.enableBI) {
                    CheatToggles.enableBIGhost = true;
                    CheatToggles.enableBIMissions = true;
                    CheatToggles.enableBIPlayer = true;
                } else {
                    CheatToggles.enableBIGhost = false;
                    CheatToggles.enableBIMissions = false;
                    CheatToggles.enableBIPlayer = false;
                }
                Debug.Msg("Basic informations: Toggled " + (CheatToggles.enableBI ? "On" : "Off"), 1);
            }

            if (keyboard.upArrowKey.wasPressedThisFrame) {
                CheatToggles.enableEsp = !CheatToggles.enableEsp;
                if (!CheatToggles.enableEspGhost && !CheatToggles.enableEspPlayer && !CheatToggles.enableEspBone && !CheatToggles.enableEspOuija && !CheatToggles.enableEspEmf && !CheatToggles.enableEspFuseBox) {
                    CheatToggles.enableEsp = true;
                }
                if (CheatToggles.enableEsp) {
                    CheatToggles.enableEspGhost = true;
                    CheatToggles.enableEspPlayer = true;
                    CheatToggles.enableEspBone = true;
                    CheatToggles.enableEspOuija = true;
                    CheatToggles.enableEspEmf = true;
                    CheatToggles.enableEspFuseBox = true;
                } else {
                    CheatToggles.enableEspGhost = false;
                    CheatToggles.enableEspPlayer = false;
                    CheatToggles.enableEspBone = false;
                    CheatToggles.enableEspOuija = false;
                    CheatToggles.enableEspEmf = false;
                    CheatToggles.enableEspFuseBox = false;
                }
                Debug.Msg("ESP: Toggled " + (CheatToggles.enableEsp ? "On" : "Off"), 1);
            }

            if (keyboard.downArrowKey.wasPressedThisFrame) {
                CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                Debug.Msg("Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"), 1);
                if (CheatToggles.enableFullbright == true) {
                    Fullbright.Enable();
                } else {
                    Fullbright.Disable();
                }
            }

            if (keyboard.insertKey.wasPressedThisFrame || keyboard.deleteKey.wasPressedThisFrame || keyboard.rightCtrlKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame) {
                CheatToggles.guiEnabled = !CheatToggles.guiEnabled;
                Debug.Msg("GUI: Toggled " + (CheatToggles.guiEnabled ? "On" : "Off"), 1);

                if (CheatToggles.guiEnabled) {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (myPlayer != null)
                        myPlayer.field_Public_FirstPersonController_0.enabled = false;
                } else {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    if (myPlayer != null)
                        myPlayer.field_Public_FirstPersonController_0.enabled = true;
                }
            }
        }
        public override void OnGUI() {
            if (!isPluginLoaded) {
                GUI.Label(new Rect(0f, 0f, 200f, 30f), "", "box");
                GUI.Label(new Rect(5f, 4f, 180f, 30f), "<color=#FFFF00><b>Plugin loading...</b></color>");
                if (myPlayer != null)
                    isPluginLoaded = true;
            }

            if (CheatToggles.guiEnabled) {
                if (initializedScene > 1) {
                    GUI.Label(new Rect(500f, 0f, 400f, 210f), "", "box");
                    if (GUI.Toggle(new Rect(500f, 2f, 150f, 20f), CheatToggles.guiGhost, "Ghost GUI") != CheatToggles.guiGhost) {
                        CheatToggles.guiGhost = !CheatToggles.guiGhost;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiGhost == true) {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableEspGhost, "Ghost ESP") != CheatToggles.enableEspGhost) {
                            CheatToggles.enableEspGhost = !CheatToggles.enableEspGhost;
                            Debug.Msg("ESP: Toggled " + (CheatToggles.enableEspGhost ? "On" : "Off"), 1);

                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 22f, 150f, 20f), CheatToggles.guiESP, "ESP GUI") != CheatToggles.guiESP) {
                        CheatToggles.guiESP = !CheatToggles.guiESP;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiESP == true) {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableEspGhost, "Ghost ESP") != CheatToggles.enableEspGhost) {
                            CheatToggles.enableEspGhost = !CheatToggles.enableEspGhost;
                            Debug.Msg("Ghost ESP: Toggled " + (CheatToggles.enableEspGhost ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), CheatToggles.enableEspGhostVisible, "Ghost Visible") != CheatToggles.enableEspGhostVisible) {
                            CheatToggles.enableEspGhostVisible = !CheatToggles.enableEspGhostVisible;
                            Debug.Msg("Ghost Visible: Toggled " + (CheatToggles.enableEspGhostVisible ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 42f, 150f, 20f), CheatToggles.enableEspGhostBone, "Ghost Bone ESP") != CheatToggles.enableEspGhostBone) {
                            CheatToggles.enableEspGhostBone = !CheatToggles.enableEspGhostBone;
                            Debug.Msg("Ghost Bone ESP: Toggled " + (CheatToggles.enableEspGhostBone ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 62f, 150f, 20f), CheatToggles.enableEspGhostFinger, "Ghost Bone Finger ESP") != CheatToggles.enableEspGhostFinger) {
                            CheatToggles.enableEspGhostFinger = !CheatToggles.enableEspGhostFinger;
                            Debug.Msg("Ghost Bone Finger ESP: Toggled " + (CheatToggles.enableEspGhostFinger ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 82f, 150f, 20f), CheatToggles.enableEspPlayer, "Player ESP") != CheatToggles.enableEspPlayer) {
                            CheatToggles.enableEspPlayer = !CheatToggles.enableEspPlayer;
                            Debug.Msg("Player ESP: Toggled " + (CheatToggles.enableEspPlayer ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 102f, 150f, 20f), CheatToggles.enableEspPlayerBone, "Player Bone ESP") != CheatToggles.enableEspPlayerBone) {
                            CheatToggles.enableEspPlayerBone = !CheatToggles.enableEspPlayerBone;
                            Debug.Msg("Player Bone ESP: Toggled " + (CheatToggles.enableEspPlayerBone ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 122f, 150f, 20f), CheatToggles.enableEspBone, "Bone ESP") != CheatToggles.enableEspBone) {
                            CheatToggles.enableEspBone = !CheatToggles.enableEspBone;
                            Debug.Msg("Bone ESP: Toggled " + (CheatToggles.enableEspBone ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 142f, 150f, 20f), CheatToggles.enableEspOuija, "Cursed Items ESP") != CheatToggles.enableEspOuija) {
                            CheatToggles.enableEspOuija = !CheatToggles.enableEspOuija;
                            Debug.Msg("Cursed Items ESP: Toggled " + (CheatToggles.enableEspOuija ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 162f, 150f, 20f), CheatToggles.enableEspFuseBox, "FuseBox ESP") != CheatToggles.enableEspFuseBox) {
                            CheatToggles.enableEspFuseBox = !CheatToggles.enableEspFuseBox;
                            Debug.Msg("FuseBox ESP: Toggled " + (CheatToggles.enableEspFuseBox ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 182f, 150f, 20f), CheatToggles.enableEspEmf, "Emf ESP") != CheatToggles.enableEspEmf) {
                            CheatToggles.enableEspEmf = !CheatToggles.enableEspEmf;
                            Debug.Msg("Emf ESP: Toggled " + (CheatToggles.enableEspEmf ? "On" : "Off"), 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 42f, 150f, 20f), CheatToggles.guiHelper, "Helper GUI") != CheatToggles.guiHelper) {
                        CheatToggles.guiHelper = !CheatToggles.guiHelper;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiHelper == true) {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.guiHelperInfo, "Basic Info") != CheatToggles.guiHelperInfo) {
                            CheatToggles.guiHelperInfo = !CheatToggles.guiHelperInfo;
                        }
                        if (CheatToggles.guiHelperInfo == true) {
                            if (GUI.Toggle(new Rect(800f, 2f, 150f, 20f), CheatToggles.enableBIGhost, "Ghost Info") != CheatToggles.enableBIGhost) {
                                CheatToggles.enableBIGhost = !CheatToggles.enableBIGhost;
                                Debug.Msg("Ghost Info: Toggled " + (CheatToggles.enableBIGhost ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(800f, 22f, 150f, 20f), CheatToggles.enableBIMissions, "Missions Info") != CheatToggles.enableBIMissions) {
                                CheatToggles.enableBIMissions = !CheatToggles.enableBIMissions;
                                Debug.Msg("Missions Info: Toggled " + (CheatToggles.enableBIMissions ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(800f, 42f, 150f, 20f), CheatToggles.enableBIPlayer, "Player Info") != CheatToggles.enableBIPlayer) {
                                CheatToggles.enableBIPlayer = !CheatToggles.enableBIPlayer;
                                Debug.Msg("Player Info: Toggled " + (CheatToggles.enableBIPlayer ? "On" : "Off"), 1);
                            }
                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), CheatToggles.enableFullbright, "Enable Fullbright") != CheatToggles.enableFullbright) {
                            CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                            Debug.Msg("Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"), 1);
                            if (CheatToggles.enableFullbright) {
                                Fullbright.Enable();
                            } else {
                                Fullbright.Disable();
                            }
                        }
                        if (GUI.Toggle(new Rect(650f, 62f, 150f, 20f), CheatToggles.enableInfStamina, "Infinite Stamina") != CheatToggles.enableInfStamina) {
                            CheatToggles.enableInfStamina = !CheatToggles.enableInfStamina;
                            Debug.Msg("Infinite Stamina: Toggled " + (CheatToggles.enableInfStamina ? "On" : "Off"), 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 62f, 150f, 20f), CheatToggles.guiTroll, "Troll GUI") != CheatToggles.guiTroll) {
                        CheatToggles.guiTroll = !CheatToggles.guiTroll;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (GUI.Toggle(new Rect(500f, 82f, 150f, 20f), CheatToggles.guiDebug, "Debug GUI") != CheatToggles.guiDebug) {
                        CheatToggles.guiDebug = !CheatToggles.guiDebug;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiDebug == true) {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableDebug, "Enable Debug") != CheatToggles.enableDebug) {
                            CheatToggles.enableDebug = !CheatToggles.enableDebug;
                            Debug.Msg("Debug: Toggled " + (CheatToggles.enableDebug ? "On" : "Off"), 1);
                            MelonPreferences.SetEntryValue<bool>("Settings", "DebugEnabled", CheatToggles.enableDebug);
                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), Debug.debugMode1, "Debug Mode 1") != Debug.debugMode1) {
                            Debug.debugMode1 = !Debug.debugMode1;
                            Debug.Msg("Debug Mode 1: Toggled " + (Debug.debugMode1 ? "On" : "Off"), 1);
                            MelonPreferences.SetEntryValue<bool>("Settings", "DebugM1Enabled", Debug.debugMode1);
                        }
                        if (GUI.Toggle(new Rect(650f, 42f, 150f, 20f), Debug.debugMode2, "Debug Mode 2") != Debug.debugMode2) {
                            Debug.debugMode2 = !Debug.debugMode2;
                            Debug.Msg("Debug Mode 2: Toggled " + (Debug.debugMode2 ? "On" : "Off"), 1);
                            MelonPreferences.SetEntryValue<bool>("Settings", "DebugM2Enabled", Debug.debugMode2);
                        }
                        if (GUI.Toggle(new Rect(650f, 62f, 150f, 20f), Debug.debugMode3, "Debug Mode 3") != Debug.debugMode3) {
                            Debug.debugMode3 = !Debug.debugMode3;
                            Debug.Msg("Debug Mode 3: Toggled " + (Debug.debugMode3 ? "On" : "Off"), 1);
                            MelonPreferences.SetEntryValue<bool>("Settings", "DebugM3Enabled", Debug.debugMode3);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 102f, 150f, 20f), CheatToggles.guiTest, "New Features") != CheatToggles.guiTest) {
                        CheatToggles.guiTest = !CheatToggles.guiTest;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiTest == true) {
                        if (GUI.Button(new Rect(650f, 82f, 150f, 20f), "Disable All Features") && levelController != null) {
                            DisableAll();
                            Debug.Msg("Disable All", 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 122f, 150f, 20f), CheatToggles.guiFeatureCollection, "Feature Coll. GUI") != CheatToggles.guiFeatureCollection) {
                        CheatToggles.guiFeatureCollection = !CheatToggles.guiFeatureCollection;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                    }
                } else {
                    if (initializedScene == 1) {
                        if (GUI.Toggle(new Rect(350f, 2f, 150f, 20f), CheatToggles.guiDebug, "Debug GUI") != CheatToggles.guiDebug) {
                            CheatToggles.guiDebug = !CheatToggles.guiDebug;
                            CheatToggles.guiGhost = false;
                            CheatToggles.guiGhostTroll = false;
                            CheatToggles.guiESP = false;
                            CheatToggles.guiHelper = false;
                            CheatToggles.guiHelperInfo = false;
                            CheatToggles.guiTroll = false;
                            CheatToggles.guiTest = false;
                            CheatToggles.guiFeatureCollection = false;
                        }
                        if (CheatToggles.guiDebug == true) {
                            if (GUI.Toggle(new Rect(370f, 22f, 150f, 20f), CheatToggles.enableDebug, "Enable Debug") != CheatToggles.enableDebug) {
                                CheatToggles.enableDebug = !CheatToggles.enableDebug;
                                Debug.Msg("Debug: Toggled " + (CheatToggles.enableDebug ? "On" : "Off"), 1);
                                MelonPreferences.SetEntryValue<bool>("Settings", "DebugEnabled", CheatToggles.enableDebug);
                            }
                            if (GUI.Toggle(new Rect(370f, 42f, 150f, 20f), Debug.debugMode1, "Debug Mode 1") != Debug.debugMode1) {
                                Debug.debugMode1 = !Debug.debugMode1;
                                Debug.Msg("Debug Mode 1: Toggled " + (Debug.debugMode1 ? "On" : "Off"), 1);
                                MelonPreferences.SetEntryValue<bool>("Settings", "DebugM1Enabled", Debug.debugMode1);
                            }
                            if (GUI.Toggle(new Rect(370f, 62f, 150f, 20f), Debug.debugMode2, "Debug Mode 2") != Debug.debugMode2) {
                                Debug.debugMode2 = !Debug.debugMode2;
                                Debug.Msg("Debug Mode 2: Toggled " + (Debug.debugMode2 ? "On" : "Off"), 1);
                                MelonPreferences.SetEntryValue<bool>("Settings", "DebugM2Enabled", Debug.debugMode2);
                            }
                            if (GUI.Toggle(new Rect(370f, 82f, 150f, 20f), Debug.debugMode3, "Debug Mode 3") != Debug.debugMode3) {
                                Debug.debugMode3 = !Debug.debugMode3;
                                Debug.Msg("Debug Mode 3: Toggled " + (Debug.debugMode3 ? "On" : "Off"), 1);
                                MelonPreferences.SetEntryValue<bool>("Settings", "DebugM3Enabled", Debug.debugMode3);
                            }
                        }

                        GUI.SetNextControlName("changeName");
                        playerName = GUI.TextArea(new Rect(650f, 2f, 150f, 20f), playerName);
                        if (GUI.Button(new Rect(800f, 2f, 150f, 20f), "Change Name")) {
                            GUI.FocusControl("changeName");
                            ChangeNickname(playerName);
                        }
                    }
                }
            }

            if (initializedScene > 1) {
                if (CheatToggles.enableEspGhost || CheatToggles.enableEspGhostVisible || CheatToggles.enableEspGhostBone || CheatToggles.enableEspPlayer || CheatToggles.enableEspBone || CheatToggles.enableEspOuija || CheatToggles.enableEspEmf || CheatToggles.enableEspFuseBox) {
                    ESP.Enable();
                }

                if (CheatToggles.enableBIGhost || CheatToggles.enableBIMissions || CheatToggles.enableBIPlayer) {
                    GUI.Label(new Rect(0f, 0f, 500f, 220f), "", "box");
                }

                if (CheatToggles.enableBIGhost) {
                    BasicInformations.EnableGhost();
                    GUI.Label(new Rect(10f, 2f, 300f, 50f), "<color=#00FF00><b>Ghost Name:</b> " + (ghostNameAge ?? "") + "</color>");
                    GUI.Label(new Rect(10f, 17f, 300f, 50f), "<color=#00FF00><b>Ghost Type:</b> " + (ghostType ?? "") + "</color>");
                    GUI.Label(new Rect(10f, 47f, 400f, 50f), "<color=#00FF00><b>Evidence:</b> " + (evidence ?? "") + "</color>");
                    GUI.Label(new Rect(10f, 32f, 300f, 50f), "<color=#00FF00><b>Ghost State:</b> " + (ghostState ?? "") + "</color>");
                    GUI.Label(new Rect(10f, 62f, 400f, 50f), "<color=#00FF00><b>Responds to:</b> " + (ghostIsShy ?? "") + "</color>");
                }


                if (CheatToggles.enableBIMissions) {
                    BasicInformations.EnableMissions();
                }

                if (CheatToggles.enableBIPlayer) {
                    BasicInformations.EnablePlayer();
                    GUI.Label(new Rect(10f, 77f, 300f, 50f), "<color=#00FF00><b>My Sanity:</b> " + (myPlayerSanity ?? "N/A") + "</color>");
                }
            }

            if (!CheatToggles.enableBIGhost && initializedScene > 1) {
                BasicInformations.DisableGhost();
            }

            if (CheatToggles.enableInfStamina) {
                myPlayer.field_Public_PlayerStamina_0.field_Protected_Boolean_1 = false;
                myPlayer.field_Public_PlayerStamina_0.field_Protected_Single_1 = 3;
            }
        }

        private void ChangeNickname(String playerName) {
            Debug.Msg("ChangeNickname: " + playerName, 1);
            if (playerName.Length > 0) {
                Debug.Msg("Set name: " + playerName, 1);
                PhotonNetwork.NickName = playerName;
                Player localPlayer = GetLocalPlayer();
                localPlayer.name = playerName;
                Photon.Realtime.Player playerPR = PhotonNetwork.LocalPlayer ?? null;
                if (playerPR != null) {
                    playerPR.nickName = playerName;
                    playerPR.NickName = playerName;
                    MelonPreferences.SetEntryValue<string>("Settings", "nickname", playerName);
                }
            }
        }

        private void DisableAll() {
            Debug.Msg("DisableAll", 3);
            CheatToggles.enableBI = false;
            CheatToggles.enableBIGhost = false;
            CheatToggles.enableBIMissions = false;
            CheatToggles.enableBIPlayer = false;

            CheatToggles.enableEsp = false;
            CheatToggles.enableEspGhost = false;
            CheatToggles.enableEspGhostBone = false;
            CheatToggles.enableEspGhostFinger = false;
            CheatToggles.enableEspGhostVisible = false;
            CheatToggles.enableEspPlayer = false;
            CheatToggles.enableEspPlayerBone = false;
            CheatToggles.enableEspBone = false;
            CheatToggles.enableEspOuija = false;
            CheatToggles.enableEspEmf = false;
            CheatToggles.enableEspFuseBox = false;

            CheatToggles.enableFullbright = false;
            CheatToggles.enableInfStamina = false;
            Fullbright.Disable();

            BasicInformations.DisableGhost();
        }

        public static Player GetLocalPlayer() {
            Debug.Msg("GetLocalPlayer", 3);
            if (players == null) {
                return null;
            }
            if (players != null) {
                if (players.Count == 0) {
                    return null;
                }
                if (players.Count == 1) {
                    return players[0];
                }
                foreach (Player player in players) {
                    if (player != null && player.field_Public_PhotonView_0 != null && player.field_Public_PhotonView_0.AmOwner == true) {
                        return player;
                    }
                }
            }
            return null;
        }

        private void HandleConfig() {
            MelonPreferences.Load();

            MelonPreferences.CreateCategory("Settings", "Settings");
            MelonPreferences.CreateEntry("Settings", "HotkeysEnabled", false, "Hotkeys Enabled");
            MelonPreferences.CreateEntry("Settings", "DebugEnabled", true, "Debug Enabled");
            MelonPreferences.CreateEntry("Settings", "DebugM1Enabled", true, "Debug M1 Enabled");
            MelonPreferences.CreateEntry("Settings", "DebugM2Enabled", false, "Debug M2 Enabled");
            MelonPreferences.CreateEntry("Settings", "DebugM3Enabled", false, "Debug M3 Enabled");
            MelonPreferences.CreateEntry("Settings", "nickname", "", "Nickname");
            MelonPreferences.Save();

            settingsExist = MelonPreferences.HasEntry("Settings", "HotkeysEnabled");
            if (settingsExist) {
                CheatToggles.enableHotkeys = MelonPreferences.GetEntryValue<bool>("Settings", "HotkeysEnabled");
                CheatToggles.enableDebug = MelonPreferences.GetEntryValue<bool>("Settings", "DebugEnabled");
                Debug.debugMode1 = MelonPreferences.GetEntryValue<bool>("Settings", "DebugM1Enabled");
                Debug.debugMode2 = MelonPreferences.GetEntryValue<bool>("Settings", "DebugM2Enabled");
                Debug.debugMode3 = MelonPreferences.GetEntryValue<bool>("Settings", "DebugM3Enabled");
            }
        }

        IEnumerator CollectGameObjects() {
            try {
                Debug.Msg("isRunningTrue", 3);
                isRunning = true;

                Debug.Msg("cameraMain", 3);
                cameraMain = Camera.main ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("dnaEvidence", 3);
                dnaEvidence = Object.FindObjectOfType<DNAEvidence>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ouijaBoard", 3);
                ouijaBoard = Object.FindObjectOfType<OuijaBoard>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("tarotCards", 3);
                Main.tarotCards = Object.FindObjectsOfType<TarotCard>().ToList<TarotCard>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("voodooDollPin", 3);
                Main.voodooDollPins = Object.FindObjectsOfType<VoodooDollPin>().ToList<VoodooDollPin>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("voodooDoll", 3);
                Main.voodooDolls = Object.FindObjectsOfType<VoodooDoll>().ToList<VoodooDoll>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("musicBox", 3);
                Main.musicBoxs = Object.FindObjectsOfType<MusicBox>().ToList<MusicBox>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("hauntedMirror", 3);
                Main.hauntedMirrors = Object.FindObjectsOfType<HauntedMirror>().ToList<HauntedMirror>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("summoningCircle", 3);
                Main.summoningCircles = Object.FindObjectsOfType<SummoningCircle>().ToList<SummoningCircle>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("fuseBox", 3);
                fuseBox = Object.FindObjectOfType<FuseBox>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("gameController", 3);
                gameController = Object.FindObjectOfType<GameController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostAI", 3);
                ghostAI = Object.FindObjectOfType<GhostAI>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostActivity", 3);
                ghostActivity = Object.FindObjectOfType<GhostActivity>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostInfo", 3);
                ghostInfo = Object.FindObjectOfType<GhostInfo>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("levelController", 3);
                levelController = Object.FindObjectOfType<LevelController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                if (Object.FindObjectOfType<Player>() != null) {
                    Debug.Msg("player", 3);
                    player = Object.FindObjectOfType<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("players", 3);
                    players = Object.FindObjectsOfType<Player>().ToList<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("myPlayer", 3);
                    myPlayer = GetLocalPlayer() ?? player;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("playerAnim", 3);
                    playerAnim = myPlayer.field_Public_Animator_0 ?? null;
                    yield return new WaitForSeconds(0.15f);

                    if (playerAnim != null) {
                        Debug.Msg("boneTransform", 3);
                        boneTransform = playerAnim.GetBoneTransform(HumanBodyBones.Head) ?? null;
                        yield return new WaitForSeconds(0.15f);
                    }
                }

                if (levelController != null) {
                    Debug.Msg("emf", 3);
                    emf = Object.FindObjectsOfType<EMF>().ToList<EMF>() ?? null;
                    yield return new WaitForSeconds(0.15f);
                }

                isRunning = false;
                yield return new WaitForSeconds(0.15f);
                Debug.Msg("-----------------------------", 3);

                yield return null;
            } finally {
                if (isRunning) {
                    Debug.Msg("Unexpected Error while collecting game objects.");
                    isRunning = false;
                }
            }
        }

        IEnumerator CollectPlayerObjects() {
            try {
                Debug.Msg("isRunningTrue", 3);
                isRunning = true;
                Debug.Msg("cameraMain", 3);
                cameraMain = Camera.main ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("gameController", 3);
                gameController = Object.FindObjectOfType<GameController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                if (Object.FindObjectOfType<Player>() != null) {
                    Debug.Msg("player", 3);
                    player = Object.FindObjectOfType<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("players", 3);
                    players = Object.FindObjectsOfType<Player>().ToList<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("myPlayer", 3);
                    myPlayer = GetLocalPlayer() ?? player;
                    yield return new WaitForSeconds(0.15f);

                    if (!nicknameChanged) {
                        nicknameChanged = true;
                        Photon.Realtime.Player playerPR = PhotonNetwork.LocalPlayer ?? null;
                        if (playerPR != null) {
                            playerName = playerPR.nickName;
                        }

                        String nicknameTxt = MelonPreferences.GetEntryValue<string>("Settings", "nickname");
                        if (!String.IsNullOrEmpty(nicknameTxt)) {
                            playerName = nicknameTxt;
                            ChangeNickname(nicknameTxt);
                        }
                    }
                    yield return new WaitForSeconds(0.15f);
                }

                isRunning = false;
                yield return new WaitForSeconds(0.15f);
                Debug.Msg("-----------------------------", 3);

                yield return null;
            } finally {
                if (isRunning) {
                    Debug.Msg("Unexpected Error while collecting game objects.");
                    isRunning = false;
                }
            }
        }

        public static Transform boneTransform = null;
        public static Camera cameraMain = null;
        public static DNAEvidence dnaEvidence = null;
        public static GameController gameController = null;
        public static OuijaBoard ouijaBoard = null;
        public static List<TarotCard> tarotCards = null;
        public static List<VoodooDollPin> voodooDollPins = null;
        public static List<VoodooDoll> voodooDolls = null;
        public static List<MusicBox> musicBoxs = null;
        public static List<HauntedMirror> hauntedMirrors = null;
        public static List<SummoningCircle> summoningCircles = null;
        public static GhostAI ghostAI = null;
        public static List<EMF> emf = null;
        public static EMFData emfData = null;
        public static FuseBox fuseBox = null;
        public static GhostActivity ghostActivity = null;
        public static GhostInteraction ghostInteraction = null;
        public static GhostController ghostController = null;
        public static GhostInfo ghostInfo = null;
        public static LevelController levelController = null;
        public static Light light = null;
        public static Player myPlayer = null;
        public static PhotonView photonView = null;
        public static Player player = null;
        public static List<Player> players = null;
        public static Animator playerAnim = null;
        public static List<Window> windows = null;
        public static String ghostNameAge = null;
        public static String ghostType = null;
        public static String evidence = null;
        public static String ghostState = null;
        public static String ghostIsShy = null;
        public static String myPlayerSanity = null;
        public static String[] mapNames = { "Opening Scene", "Lobby", "Tanglewood Street", "Ridgeview Road House", "Edgefield Street House", "Asylum", "Brownstone High School", "Bleasdale Farmhouse", "Grafton Farmhouse", "Prison", "Willow Street House", "Maple Lodge Campsite" };
        public static String inSight = "";
        public static bool settingsExist = false;
        public static int initializedScene;
        private static bool gameStarted = false;
        private static object coRoutine = null;
        private static bool canRun = true;
        private static bool isRunning = false;
        private static String playerName = null;
        private static bool nicknameChanged = false;
        private static bool isPluginLoaded = false;
    }
}
