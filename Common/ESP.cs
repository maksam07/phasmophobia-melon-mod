using UnityEngine;

namespace C4PhasMod
{
    class ESP
    {
        public static void Enable()
        {
            if (Main.initializedScene > 1)
            {
                if (CheatToggles.enableEspGhost == true && Main.gameController != null && Main.ghostAI != null && Main.ghostAIs.Count > 0)
                {
                    Vector3 w2s = Main.cameraMain.WorldToScreenPoint(Main.ghostAI.transform.position);
                    Vector3 ghostPosition = Main.cameraMain.WorldToScreenPoint(Main.ghostAI.field_Public_Transform_0.transform.position);

                    Vector3 w2s2 = Main.ghostAI.transform.position;
                    Vector3 ghostPosition2 = Main.ghostAI.field_Public_Transform_0.transform.position;

                    float ghostNeckMid = Screen.height - ghostPosition.y;
                    float ghostBottomMid = Screen.height - w2s.y;
                    float ghostTopMid = ghostNeckMid - (ghostBottomMid - ghostNeckMid) / 5;
                    float boxHeight = (ghostBottomMid - ghostTopMid);
                    float boxWidth = boxHeight / 1.8f;

                    if (w2s.z >= 0)
                        Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), ghostNeckMid), boxWidth, boxHeight, Color.cyan);
                }

                if (CheatToggles.enableEspGhostVisible == true && Main.gameController != null && Main.ghostAI != null)
                {
                    Main.ghostAI.Appear(false);
                    Main.ghostAI.field_Public_SanityDrainer_0.enabled = false;
                    //Code from wh0am15533 (https://www.unknowncheats.me/forum/members/2860743.html)
                    if (Main.ghostAI.ghostParticle != null)
                    {
                        Main.ghostAI.ghostParticle.GetComponent<ParticleSystemRenderer>().enabled = true;
                    }
                }

                if (CheatToggles.enableEspGhostBone && Main.gameController != null && Main.ghostAI != null)
                {
                    GUIStyle guiStyle = new GUIStyle();
                    GUI.color = Color.cyan;
                    guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;
                    Transform[] bones = new Transform[55];
                    Vector3[] bonesPos = new Vector3[55];
                    int i = 0; int x = 0;
                    foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones)))
                    {
                        if (i < 55)
                        {
                            try
                            {
                                bones[i] = Main.ghostAI.field_Public_Animator_0.GetBoneTransform(bone) ?? null;
                                if (bones[i] != null)
                                {
                                    bonesPos[x] = Main.cameraMain.WorldToScreenPoint(bones[i].position);
                                    if (bonesPos[x].z < 0)
                                        break;
                                    GUI.DrawTexture(new Rect(bonesPos[x].x, (float)Screen.height - bonesPos[x].y, 5, 5), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                                    x++;
                                }
                            }
                            catch (System.Exception e)
                            {
                                Debug.Msg("Exception: " + e, 3);
                            }
                        }
                        i++;
                    }
                }

                if (CheatToggles.enableEspPlayer == true && Main.gameController != null && Main.players != null && Main.players.Count > 1)
                {
                    foreach (Player player in Main.players)
                    {
                        if (player.field_Public_PhotonView_0 != null && player.field_Public_PhotonView_0.AmOwner)
                            continue;

                        Vector3 w2s = Main.cameraMain.WorldToScreenPoint(player.transform.position);
                        Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(player.field_Public_Transform_1.transform.position);

                        float playerNeckMid = Screen.height - playerPosition.y;
                        float playerBottomMid = Screen.height - w2s.y;
                        float playerTopMid = playerNeckMid - (playerBottomMid - playerNeckMid) / 5;
                        float boxHeight = (playerBottomMid - playerTopMid);
                        float boxWidth = boxHeight / 1.8f;

                        if (w2s.z < 0)
                            continue;

                        Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), playerNeckMid), boxWidth, boxHeight, Color.green);
                        GUI.Label(new Rect(new Vector2(w2s.x, Screen.height - (w2s.y + 1f)), new Vector2(100f, 100f)), player.field_Public_PhotonView_0.Owner.NickName);
                    }
                }

                if (CheatToggles.enableEspBone == true && Main.gameController != null && Main.dnaEvidences != null && Main.dnaEvidences.Count > 0)
                {
                    foreach (DNAEvidence dnaEvidence in Main.dnaEvidences)
                    {
                        Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(dnaEvidence.transform.position);
                        if (vector3.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#FFFFFF><b>Bone</b></color>");
                        }
                    }
                }

                if (CheatToggles.enableEspOuija == true && Main.gameController != null && Main.ouijaBoards != null && Main.ouijaBoards.Count > 0)
                {
                    foreach (OuijaBoard ouijaBoard in Main.ouijaBoards)
                    {
                        Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(ouijaBoard.transform.position);
                        if (vector2.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#D11500><b>Ouija Board</b></color>");
                        }
                    }
                }

                if (CheatToggles.enableEspEmf == true && Main.gameController != null && Main.emf != null && Main.emf.Count > 0)
                {
                    foreach (EMF emf in Main.emf)
                    {
                        Vector3 vector = Camera.main.WorldToScreenPoint(emf.transform.position);
                        if (vector.z > 0f)
                        {
                            vector.y = Screen.height - (vector.y + 1f);
                            GUI.color = new Color32(210, 31, 255, 255);
                            string spotName = "";

                            switch ((int)emf.field_Public_EnumNPublicSealedvaGh5vGhGhGhUnique_0)
                            {
                                case 0:
                                    spotName = "EMF: Interaction";
                                    break;
                                case 1:
                                    spotName = "EMF: Thrown";
                                    break;
                                case 2:
                                    spotName = "EMF: Appeared";
                                    break;
                                case 3:
                                    spotName = "EMF: Evidence";
                                    break;
                            }
                            GUI.Label(new Rect(new Vector2(vector.x, vector.y), new Vector2(100f, 100f)), spotName);
                        }
                    }
                }

                if (CheatToggles.enableEspFuseBox == true && Main.gameController != null && Main.fuseBox != null)
                {
                    Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.fuseBox.transform.position);
                    if (vector3.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#EBC634><b>FuseBox</b></color>");
                    }
                }
            }
        }
    }
}
