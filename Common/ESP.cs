using UnityEngine;

namespace C4PhasMod
{
    class ESP
    {
        public static void Enable()
        {
            if (Main.initializedScene > 1) {
                if (CheatToggles.enableEspGhost == true && Main.gameController != null && Main.ghostAI != null && Main.ghostAIs.Count > 0)
                {
                    foreach (GhostAI ghostAI in Main.ghostAIs)
                    {
                        Vector3 w2s = Main.cameraMain.WorldToScreenPoint(ghostAI.transform.position);
                        Vector3 ghostPosition = Main.cameraMain.WorldToScreenPoint(ghostAI.field_Public_Transform_0.transform.position);

                        Vector3 w2s2 = ghostAI.transform.position;
                        Vector3 ghostPosition2 = ghostAI.field_Public_Transform_0.transform.position;

                        float ghostNeckMid = Screen.height - ghostPosition.y;
                        float ghostBottomMid = Screen.height - w2s.y;
                        float ghostTopMid = ghostNeckMid - (ghostBottomMid - ghostNeckMid) / 5;
                        float boxHeight = (ghostBottomMid - ghostTopMid);
                        float boxWidth = boxHeight / 1.8f;

                        if (w2s.z < 0)
                            continue;

                        Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), ghostNeckMid), boxWidth, boxHeight, Color.cyan);
                        //ghostAI.field_Public_EnumNPublicSealedvaidwahufalidothfuapUnique_0 = GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.appear;
                        //ghostAI.field_Public_Single_0 = 100f;
                        //ghostAI.field_Public_Boolean_1 = true;
                    }
                    //Vector3 w2sP = Main.cameraMain.WorldToScreenPoint(Main.myPlayer.transform.position);
                    //Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(Main.myPlayer.field_Public_Transform_1.transform.position);
                }

                /*if (1==2 && CheatToggles.enableEspGhostBone == true && Main.gameController != null && Main.ghostAI != null && Main.ghostAIs.Count > 0)
                {
                    foreach (GhostAI ghostAI in Main.ghostAIs)
                    {
                        Debug.Msg("Head", 1);
                        Transform boneHead              = ghostAI.field_Public_Animator_0.GetBoneTransform(HumanBodyBones.Head) ?? null;
                        Debug.Msg("Neck", 1);
                        Transform boneNeck              = ghostAI.field_Public_Animator_0.GetBoneTransform(HumanBodyBones.Neck) ?? null;
                        Debug.Msg("Spine", 1);
                        Transform boneSpine             = ghostAI.field_Public_Animator_0.GetBoneTransform(HumanBodyBones.Spine) ?? null;
                        Debug.Msg("UpperChest", 1);
                        Transform boneUpperChest        = ghostAI.field_Public_Animator_0.GetBoneTransform(HumanBodyBones.UpperChest) ?? null;
                        Debug.Msg("Chest", 1);
                        Transform boneChest             = ghostAI.field_Public_Animator_0.GetBoneTransform(HumanBodyBones.Chest) ?? null;
                        Vector3 bone1;
                        Vector3 bone2;
                        Vector3 bone3;
                        Vector3 bone4;
                        Vector3 bone5;



                        if (boneHead != null)
                        {
                            bone1 = Main.cameraMain.WorldToScreenPoint(boneHead.position);
                        }
                        else
                        {
                            bone1 = new Vector3(0, 0, 0);
                        }
                            
                        if (boneNeck != null)
                        {
                            bone2 = Main.cameraMain.WorldToScreenPoint(boneNeck.position);
                        }
                        else
                        {
                            bone2 = new Vector3(0, 0, 0);
                        }

                        if (boneSpine != null)
                        {
                            bone3 = Main.cameraMain.WorldToScreenPoint(boneSpine.position);
                        }
                        else
                        {
                            bone3 = new Vector3(0, 0, 0);
                        }

                        if (boneUpperChest != null)
                        {
                            bone4 = Main.cameraMain.WorldToScreenPoint(boneUpperChest.position);
                        }
                        else
                        {
                            bone4 = new Vector3(0, 0, 0);
                        }

                        if (boneChest != null)
                        {
                            bone5 = Main.cameraMain.WorldToScreenPoint(boneChest.position);
                        }
                        else
                        {
                            bone5 = new Vector3(0, 0, 0);
                        }

                        if (bone1.z < 0)
                            continue;

                        if(bone1.x != 0 && bone2.x != 0)
                            Drawing.DrawLine(new Vector2(bone1.x, (float)Screen.height - bone1.y), new Vector2(bone2.x, (float)Screen.height - bone2.y), Color.green, 2f);
                        if (bone2.x != 0 && bone3.x != 0)
                            Drawing.DrawLine(new Vector2(bone2.x, (float)Screen.height - bone2.y), new Vector2(bone3.x, (float)Screen.height - bone3.y), Color.red, 2f);
                        if (bone3.x != 0 && bone4.x != 0)
                            Drawing.DrawLine(new Vector2(bone3.x, (float)Screen.height - bone3.y), new Vector2(bone3.x, (float)Screen.height - bone4.y), Color.blue, 2f);
                        if (bone4.x != 0 && bone5.x != 0)
                            Drawing.DrawLine(new Vector2(bone4.x, (float)Screen.height - bone4.y), new Vector2(bone5.x, (float)Screen.height - bone5.y), Color.cyan, 2f);
                        //Drawing.DrawLine(new Vector2(bone1.x, (float)Screen.height - bone1.y), new Vector2(bone2.x, (float)Screen.height - bone2.y), Color.green, 2f);
                    }
                }*/

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
