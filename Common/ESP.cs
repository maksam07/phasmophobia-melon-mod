using System;
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
                    try
                    {
                        Vector3 ghostHeadPos = ESP.getBonePos(HumanBodyBones.Head, Main.ghostAI.field_Public_Animator_0);
                        float distance = ESP.getDistance(Main.myPlayer.transform.position, Main.ghostAI.transform.position);
                        ghostHeadPos += new Vector3(0, 25, 0);
                        float boxHeight = ESP.getDistance(ghostHeadPos, Main.cameraMain.WorldToScreenPoint(Main.ghostAI.transform.position));
                        float boxWidth = (boxHeight / 2f);


                        if (ghostHeadPos.z > 0)
                        {
                            Drawing.DrawBoxOutline(new Vector2(ghostHeadPos.x, Screen.height - (ghostHeadPos.y - boxWidth)), boxWidth, boxHeight, Color.cyan);
                            GUIStyle guiStyle = new GUIStyle();
                            GUI.color = Color.cyan;
                            guiStyle.normal.textColor = Color.cyan; guiStyle.fontSize = (int)boxHeight / 9;
                            if (guiStyle.fontSize < 8) guiStyle.fontSize = 8;
                            if (guiStyle.fontSize > 16) guiStyle.fontSize = 16;
                            GUI.Label(new Rect(new Vector2(ghostHeadPos.x + (boxWidth / 2), Screen.height - ghostHeadPos.y + boxHeight), new Vector2(100f, 100f)), Math.Round((decimal)distance, 2).ToString(), guiStyle);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Msg("Ghost ESP: " + e, 4);
                    }
                }

                if (CheatToggles.enableEspGhostVisible == true && Main.gameController != null && Main.ghostAI != null)
                {
                    try
                    {
                        Main.ghostAI.Appear(Main.myPlayer);
                    }
                    catch (Exception e)
                    {
                        Debug.Msg("Ghost Visible Appear: " + e, 4);
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
                }

                if (CheatToggles.enableEspPlayer == true && Main.gameController != null && Main.players != null && Main.players.Count > 1)
                {
                    foreach (Player player in Main.players)
                    {
                        try
                        {
                            if (player.field_Public_PhotonView_0 != null && player.field_Public_PhotonView_0.AmOwner)
                                continue;

                            Vector3 playerHeadPos = ESP.getBonePos(HumanBodyBones.Head, player.field_Public_Animator_0);
                            float distance = ESP.getDistance(Main.myPlayer.transform.position, player.transform.position);
                            playerHeadPos += new Vector3(0, 25, 0);
                            float boxHeight = ESP.getDistance(playerHeadPos, Main.cameraMain.WorldToScreenPoint(player.transform.position));
                            float boxWidth = (boxHeight / 2f);

                            if (playerHeadPos.z <= 0)
                                break;

                            Drawing.DrawBoxOutline(new Vector2(playerHeadPos.x, Screen.height - (playerHeadPos.y - boxWidth)), boxWidth, boxHeight, Color.cyan);
                            GUIStyle guiStyle = new GUIStyle();
                            GUI.color = Color.cyan;
                            guiStyle.fontSize = (int)(35 / distance); guiStyle.fontSize = 35; guiStyle.normal.textColor = Color.cyan;
                            GUI.Label(new Rect(new Vector2(playerHeadPos.x + (boxWidth / 2), Screen.height - playerHeadPos.y + boxHeight), new Vector2(100f, 100f)), Math.Round((decimal)distance, 2).ToString(), guiStyle);
                        }
                        catch (Exception e)
                        {
                            Debug.Msg("Player ESP(): " + e, 4);
                        }
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
                    try
                    {
                        Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.dnaEvidence.transform.position);
                        if (vector3.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#FFFFFF><b>Bone</b></color>");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Msg("Bone ESP(): " + e, 4);
                    }
                }

                if (CheatToggles.enableEspOuija == true && Main.gameController != null && Main.ouijaBoard != null)
                {
                    try
                    {
                        Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(Main.ouijaBoard.transform.position);
                        if (vector2.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#D11500><b>Ouija Board</b></color>");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Msg("Ouija ESP(): " + e, 4);
                    }
                }

                if (CheatToggles.enableEspEmf == true && Main.gameController != null && Main.emf != null && Main.emf.Count > 0)
                {
                    foreach (EMF emf in Main.emf)
                    {
                        try
                        {
                            Vector3 vector = Camera.main.WorldToScreenPoint(emf.transform.position);
                            if (vector.z > 0f)
                            {
                                vector.y = Screen.height - (vector.y + 1f);
                                GUI.color = new Color32(210, 31, 255, 255);
                                string spotName = "";

                                switch (emf.field_Public_Int32_0)
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
                        catch (Exception e)
                        {
                            Debug.Msg("EMF ESP(): " + e, 4);
                        }
                    }
                }

                if (CheatToggles.enableEspFuseBox == true && Main.gameController != null && Main.fuseBox != null)
                {
                    try
                    {
                        Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.fuseBox.transform.position);
                        if (vector3.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#EBC634><b>FuseBox</b></color>");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Msg("Fusebox ESP: " + e, 4);
                    }
                }
            }
        }

        private static void ProcessBones(Animator boneSource)
        {
            GUIStyle guiStyle = new GUIStyle();
            GUI.color = Color.cyan;
            guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;
            float distance = ESP.getDistance(Main.myPlayer.transform.position, Main.ghostAI.transform.position);
            distance = (distance / 5);
            if (distance < 1) distance = 1;

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
                try
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
                        GUI.DrawTexture(new Rect(bonePos.x - 8f, (float)Screen.height - bonePos.y - 15f, 16 / distance, 30 / distance), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                    else
                        GUI.DrawTexture(new Rect(bonePos.x - 3f, (float)Screen.height - bonePos.y - 3f, 6 / distance, 6 / distance), Texture2D.whiteTexture, ScaleMode.StretchToFill);

                    if (i + 1 <= 25 && bonesToDraw[i + 1] != HumanBodyBones.LastBone)
                    {
                        Vector3 nextBone = ESP.getBonePos(bonesToDraw[i + 1], boneSource);

                        if (bonePos.x != 0 && nextBone.x != 0)
                            Drawing.DrawLine(new Vector2(bonePos.x, (float)Screen.height - bonePos.y), new Vector2(nextBone.x, (float)Screen.height - nextBone.y), Color.cyan, 3 / distance);
                    }
                }
                catch (Exception e)
                {
                    Debug.Msg("ProcessBones(): " + e, 4);
                }
                i++;
            }
        }

        private static void ProcessFingers(Animator boneSource)
        {
            GUIStyle guiStyle = new GUIStyle();
            GUI.color = Color.cyan;
            guiStyle.fontSize = 15; guiStyle.normal.textColor = Color.cyan;
            float distance = ESP.getDistance(Main.myPlayer.transform.position, Main.ghostAI.transform.position);
            distance = (distance / 5);
            if (distance < 1) distance = 1;

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
                try
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
                        GUI.DrawTexture(new Rect(bonePos.x - 8f, (float)Screen.height - bonePos.y - 15f, 16 / distance, 30 / distance), Texture2D.whiteTexture, ScaleMode.StretchToFill);
                    else
                        GUI.DrawTexture(new Rect(bonePos.x - 3f, (float)Screen.height - bonePos.y - 3f, 6 / distance, 6 / distance), Texture2D.whiteTexture, ScaleMode.StretchToFill);

                    if (i + 1 <= 25 && bonesToDraw[i + 1] != HumanBodyBones.LastBone)
                    {
                        Vector3 nextBone = ESP.getBonePos(bonesToDraw[i + 1], boneSource);

                        if (bonePos.x != 0 && nextBone.x != 0)
                            Drawing.DrawLine(new Vector2(bonePos.x, (float)Screen.height - bonePos.y), new Vector2(nextBone.x, (float)Screen.height - nextBone.y), Color.cyan, 3 / distance);
                    }
                }
                catch (Exception e)
                {
                    Debug.Msg("ProcessFingers(): " + e, 4);
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
            catch (Exception e)
            {
                Debug.Msg("Exception: " + e, 3);
            }
            return (boneTransf != null) ? Main.cameraMain.WorldToScreenPoint(boneTransf.position) : new Vector3(0, 0, 0);
        }

        public static float getDistance(Vector3 pointA, Vector3 pointB)
        {
            return (pointB - pointA).magnitude;
        }

        public float FloatSQRT(float a)
        {
            return (float)Math.Sqrt(a);
        }
    }
}
