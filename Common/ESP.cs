using UnityEngine;

namespace C4PhasMod
{
    class ESP
    {
        public static void Enable()
        {
            if (Main.initializedScene > 1)
            {
                if (CheatToggles.enableEspGhost == true && Main.gameController != null && Main.ghostAI != null)
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
                    try
                    {
                        Main.ghostAI.Appear(Main.GetLocalPlayer());
                    }
                    catch (System.Exception e)
                    {
                        Debug.Msg("Exception: " + e, 3);
                    }

                    Main.ghostAI.field_Public_SanityDrainer_0.enabled = false;
                    //Code from wh0am15533 (https://www.unknowncheats.me/forum/members/2860743.html)
                    if (Main.ghostAI.field_Public_Animator_0 != null)
                    {
                        ParticleSystemRenderer psRenderer = Main.ghostAI.field_Public_Animator_0.GetComponent<ParticleSystemRenderer>() ?? null;
                        if (psRenderer != null) psRenderer.enabled = true;
                    }
                }

                if (CheatToggles.enableEspGhostBone && Main.gameController != null && Main.ghostAI != null)
                {
                    ESP.ProcessBones(Main.ghostAI.field_Public_Animator_0);
                    if (CheatToggles.enableEspGhostFinger) ESP.ProcessFingers(Main.ghostAI.field_Public_Animator_0);

                    /* DEBUGGING CODE */
                    //GUIStyle guiStyle = new GUIStyle();
                    //GUI.color = Color.cyan;
                    //guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;
                    //Transform bonesTrans;
                    //Vector3 bonesPos;
                    //int i = 0;
                    //foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones)))
                    //{
                    //    if (i < 55)
                    //    {
                    //        try
                    //        {
                    //            bonesTrans = Main.ghostAI.field_Public_Animator_0.GetBoneTransform(bone) ?? null;
                    //            if (bonesTrans != null)
                    //            {
                    //                bonesPos = Main.cameraMain.WorldToScreenPoint(bonesTrans.position);
                    //                if (bonesPos.z < 0)
                    //                    break;
                    //                GUI.DrawTexture(new Rect(bonesPos.x, Screen.height - bonesPos.y, 5, 5), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                    //                GUI.Label(new Rect(new Vector2(bonesPos.x, Screen.height - bonesPos.y), new Vector2(100f, 100f)), i.ToString());
                    //            }
                    //        }
                    //        catch (System.Exception e)
                    //        {
                    //            Debug.Msg("Exception: " + e, 3);
                    //        }
                    //    }
                    //    i++;
                    //}
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

                if (CheatToggles.enableEspPlayerBone == true && Main.gameController != null && Main.players != null && Main.players.Count > 1)
                {
                    foreach (Player player in Main.players)
                    {
                        ESP.ProcessBones(player.field_Public_Animator_0);
                    }
                }

                if (CheatToggles.enableEspBone == true && Main.gameController != null && Main.dnaEvidence != null)
                {
                    Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.dnaEvidence.transform.position);
                    if (vector3.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#FFFFFF><b>Bone</b></color>");
                    }
                }

                if (CheatToggles.enableEspOuija == true && Main.gameController != null && Main.ouijaBoard != null)
                {

                    Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(Main.ouijaBoard.transform.position);
                    if (vector2.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#D11500><b>Ouija Board</b></color>");
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

        private static void ProcessBones(Animator boneSource)
        {
            GUIStyle guiStyle = new GUIStyle();
            GUI.color = Color.cyan;
            guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;

            HumanBodyBones[] bonesToDraw =
            {
                HumanBodyBones.Head,
                HumanBodyBones.Neck,
                HumanBodyBones.RightShoulder,
                HumanBodyBones.RightUpperArm,
                HumanBodyBones.RightLowerArm,
                HumanBodyBones.RightHand,
                HumanBodyBones.LastBone,
                HumanBodyBones.Neck,
                HumanBodyBones.LeftShoulder,
                HumanBodyBones.LeftUpperArm,
                HumanBodyBones.LeftLowerArm,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LastBone,
                HumanBodyBones.Neck,
                HumanBodyBones.Chest,
                HumanBodyBones.Spine,
                HumanBodyBones.RightUpperLeg,
                HumanBodyBones.RightLowerLeg,
                HumanBodyBones.RightFoot,
                HumanBodyBones.RightToes,
                HumanBodyBones.LastBone,
                HumanBodyBones.Spine,
                HumanBodyBones.LeftUpperLeg,
                HumanBodyBones.LeftLowerLeg,
                HumanBodyBones.LeftFoot,
                HumanBodyBones.LeftToes,
                HumanBodyBones.LastBone
            };

            int i = 0;
            foreach (HumanBodyBones boneToDraw in bonesToDraw)
            {
                if (boneToDraw == HumanBodyBones.LastBone)
                {
                    i++;
                    continue;
                }
                Vector3 bonePos = ESP.getBonePos(boneToDraw, boneSource);
                if (bonePos.z < 0)
                    continue;

                if (boneToDraw == HumanBodyBones.Head)
                    GUI.DrawTexture(new Rect(bonePos.x - 8f, (float)Screen.height - bonePos.y - 15f, 16, 30), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                else
                    GUI.DrawTexture(new Rect(bonePos.x - 2.5f, (float)Screen.height - bonePos.y - 2.5f, 5, 5), Texture2D.whiteTexture, ScaleMode.StretchToFill);

                if (i + 1 <= 25 && bonesToDraw[i + 1] != HumanBodyBones.LastBone)
                {
                    Vector3 nextBone = ESP.getBonePos(bonesToDraw[i + 1], boneSource);

                    if (bonePos.x != 0 && nextBone.x != 0)
                        Drawing.DrawLine(new Vector2(bonePos.x, (float)Screen.height - bonePos.y), new Vector2(nextBone.x, (float)Screen.height - nextBone.y), Color.cyan, 2);
                }
                i++;
            }
        }

        private static void ProcessFingers(Animator boneSource)
        {
            GUIStyle guiStyle = new GUIStyle();
            GUI.color = Color.cyan;
            guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;

            HumanBodyBones[] bonesToDraw =
            {
                HumanBodyBones.RightHand,
                HumanBodyBones.RightThumbProximal,
                HumanBodyBones.RightThumbIntermediate,
                HumanBodyBones.RightThumbDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.RightHand,
                HumanBodyBones.RightIndexProximal,
                HumanBodyBones.RightIndexIntermediate,
                HumanBodyBones.RightIndexDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.RightHand,
                HumanBodyBones.RightMiddleProximal,
                HumanBodyBones.RightMiddleIntermediate,
                HumanBodyBones.RightMiddleDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.RightHand,
                HumanBodyBones.RightRingProximal,
                HumanBodyBones.RightRingIntermediate,
                HumanBodyBones.RightRingDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.RightHand,
                HumanBodyBones.RightLittleProximal,
                HumanBodyBones.RightLittleIntermediate,
                HumanBodyBones.RightLittleDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftThumbProximal,
                HumanBodyBones.LeftThumbIntermediate,
                HumanBodyBones.LeftThumbDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftIndexProximal,
                HumanBodyBones.LeftIndexIntermediate,
                HumanBodyBones.LeftIndexDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftMiddleProximal,
                HumanBodyBones.LeftMiddleIntermediate,
                HumanBodyBones.LeftMiddleDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftRingProximal,
                HumanBodyBones.LeftRingIntermediate,
                HumanBodyBones.LeftRingDistal,
                HumanBodyBones.LastBone,
                HumanBodyBones.LeftHand,
                HumanBodyBones.LeftLittleProximal,
                HumanBodyBones.LeftLittleIntermediate,
                HumanBodyBones.LeftLittleDistal,
                HumanBodyBones.LastBone
            };

            int i = 0;
            foreach (HumanBodyBones boneToDraw in bonesToDraw)
            {
                if (boneToDraw == HumanBodyBones.LastBone)
                {
                    i++;
                    continue;
                }
                Vector3 bonePos = ESP.getBonePos(boneToDraw, boneSource);
                if (bonePos.z < 0)
                    continue;

                if (boneToDraw == HumanBodyBones.Head)
                    GUI.DrawTexture(new Rect(bonePos.x - 8f, (float)Screen.height - bonePos.y - 15f, 16, 30), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                else
                    GUI.DrawTexture(new Rect(bonePos.x - 2.5f, (float)Screen.height - bonePos.y - 2.5f, 5, 5), Texture2D.whiteTexture, ScaleMode.StretchToFill);

                if (i + 1 <= 25 && bonesToDraw[i + 1] != HumanBodyBones.LastBone)
                {
                    Vector3 nextBone = ESP.getBonePos(bonesToDraw[i + 1], boneSource);

                    if (bonePos.x != 0 && nextBone.x != 0)
                        Drawing.DrawLine(new Vector2(bonePos.x, (float)Screen.height - bonePos.y), new Vector2(nextBone.x, (float)Screen.height - nextBone.y), Color.cyan, 2);
                }
                i++;
            }
        }

        private static Vector3 getBonePos(HumanBodyBones bone, Animator boneSource)
        {
            Transform boneTransf = null;
            try
            {
                boneTransf = boneSource.GetBoneTransform(bone);
            }
            catch (System.Exception e)
            {
                Debug.Msg("Exception: " + e, 3);
            }
            return (boneTransf != null) ? Main.cameraMain.WorldToScreenPoint(boneTransf.position) : new Vector3(0, 0, 0);
        }
    }
}
