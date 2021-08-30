using MelonLoader;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Object = Il2CppSystem.Object;
using Boolean = Il2CppSystem.Boolean;
using Int32 = Il2CppSystem.Int32;

namespace C4PhasMod
{
    class Trolling
    {
        public static void Hunt()
        {
            Debug.Msg("Troll->Hunt: Triggered", 1);
            CloseAllExitDoors();
            Main.ghostAI.field_Public_Boolean_0 = true;
            Main.ghostAI.field_Public_Boolean_1 = true;
            Main.ghostAI.field_Public_Boolean_2 = true;
            Main.ghostAI.field_Public_Boolean_3 = true;
            Main.ghostAI.field_Public_Boolean_4 = true;
            Main.ghostAI.field_Public_Boolean_5 = true;
            Main.ghostAI.field_Public_Player_0 = null;
            //Main.ghostAI.ChasingPlayer(true); Method_Public_Void_Boolean_0 Method_Public_Void_Boolean_1
            Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", false);
            Main.ghostAI.field_Public_Animator_0.SetInteger("WalkType", 1);
            Main.ghostAI.field_Public_GhostAudio_0.TurnOnOrOffAppearSource(true);
            Main.ghostAI.field_Public_GhostAudio_0.PlayOrStopAppearSource(true);
            Main.ghostAI.field_Public_NavMeshAgent_0.speed = Main.ghostAI.field_Public_Single_0;
            Main.ghostAI.field_Public_NavMeshAgent_0.SetDestination(getDestination());
            Main.ghostAI.ChangeState(GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.hunting, null, null);
            Main.ghostAI.field_Public_GhostInteraction_0.CreateAppearedEMF(Main.ghostAI.transform.position);
            Main.ghostAI.Appear(true);
            PhotonView photonView = Main.ghostAI.field_Public_PhotonView_0;
            photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, true));
            photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, true));
        }

        public static void Idle()
        {
            Debug.Msg("Troll->Idle: Triggered", 1);
            Main.ghostAI.field_Public_Player_0 = null;
            Main.ghostAI.field_Public_Animator_0.SetInteger("IdleNumber", Random.Range(0, 2));
            Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", true);
            Main.ghostAI.field_Public_GhostAudio_0.TurnOnOrOffAppearSource(false);
            Main.ghostAI.field_Public_GhostAudio_0.PlayOrStopAppearSource(false);
            //Main.ghostAI.ChasingPlayer(false);
            Main.ghostAI.StopGhostFromHunting();
            Main.ghostAI.UnAppear(false);
            Main.ghostAI.ChangeState(GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.idle, null, null);
            PhotonView photonView = Main.ghostAI.field_Public_PhotonView_0;
            photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, false));
            photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, false));
            OpenAllExitDoors();
        }

        public static void Appear()
        {
            Debug.Msg("Troll->Appear: Triggered", 1);
            PhotonView photonView = Main.ghostAI.field_Public_PhotonView_0;
            photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, true, 0, 1));
        }

        public static void UnAppear()
        {
            Debug.Msg("Troll->UnAppear: Triggered", 1);
            PhotonView photonView = Main.ghostAI.field_Public_PhotonView_0;
            photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, false, 0, 1));
        }
        public static void Interact()
        {
            Debug.Msg("Troll->Interact: Triggered", 1);
            Main.ghostAI.RandomEvent();
            Main.ghostActivity.Interact();
            Main.ghostAI.field_Public_GhostActivity_0.InteractWithARandomProp();
            Main.ghostAI.field_Public_GhostActivity_0.Interact();
        }

        public static void LockDoors(int lockState, int howMany = 0)
        {
            switch (lockState)
            {
                case 1:
                    Debug.Msg("Troll->CloseAllExitDoors", 1);
                    CloseAllExitDoors();
                    break;
                case 2:
                    Debug.Msg("Troll->CloseAllRoomDoors", 1);
                    CloseAllRoomDoors();
                    break;
                case 3:
                    Debug.Msg("Troll->OpenAllExitDoors", 1);
                    OpenAllExitDoors();
                    break;
                case 4:
                    Debug.Msg("Troll->OpenAllRoomDoors", 1);
                    OpenAllRoomDoors();
                    break;
                default:
                    break;
            }
        }

        public static void CloseAllExitDoors()
        {
            Debug.Msg("CloseAllExitDoors", 3);
            foreach (Door exitDoor in Main.levelController.field_Public_ArrayOf_Door_1)
            {
                CloseLockDoor(exitDoor);
            }
        }
        public static void CloseAllRoomDoors()
        {
            Debug.Msg("CloseAllRoomDoors", 3);
            foreach (Door door in Main.doors)
            {
                CloseLockDoor(door);
            }
        }
        public static void OpenAllExitDoors()
        {
            Debug.Msg("OpenAllExitDoors", 3);
            foreach (Door exitDoor in Main.levelController.field_Public_ArrayOf_Door_1)
            {
                OpenUnlockDoor(exitDoor);
            }
        }
        public static void OpenAllRoomDoors()
        {
            Debug.Msg("OpenAllRoomDoors", 3);
            foreach (Door door in Main.doors)
            {
                OpenUnlockDoor(door);
            }
        }

        private static void CloseLockDoor(Door door)
        {
            Debug.Msg("CloseLockDoor", 3);
            PhotonView photonView = door.field_Public_PhotonView_0;
            door.DisableOrEnableDoor(false);
            door.LockDoor();
            door.transform.localRotation = Quaternion.identity;
            Quaternion localRotation = door.transform.localRotation;
            Vector3 eulerAngles = localRotation.eulerAngles;
            eulerAngles.y = door.field_Public_Single_0;
            localRotation.eulerAngles = eulerAngles;
            door.transform.localRotation = localRotation;
            photonView.RPC("SyncLockState", RpcTarget.All, getRPCObject(1, true));
            photonView.RPC("NetworkedPlayLockSound", RpcTarget.All, getRPCObject(0, false));
        }

        private static void OpenUnlockDoor(Door door)
        {
            Debug.Msg("OpenUnlockDoor", 3);
            PhotonView photonView = door.field_Public_PhotonView_0;
            door.DisableOrEnableDoor(true);
            door.DisableOrEnableCollider(true);
            door.UnlockDoor();
            photonView.RPC("SyncLockState", RpcTarget.All, getRPCObject(1, false));
        }
        public static void EventDoorKnock()
        {
            Debug.Msg("Troll->Event: Door knock", 1);
            PhotonView photonView1 = Main.soundController.view;
            photonView1.RPC("PlayDoorKnockingSound", RpcTarget.All, getRPCObject(0, false));
        }

        private static Vector3 getDestination()
        {
            Debug.Msg("getDestination", 3);
            Vector3 destination = Vector3.zero;
            float num = Random.Range(2f, 15f);
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(Random.insideUnitSphere * num + Main.ghostAI.transform.position, out navMeshHit, num, 1))
            {
                destination = navMeshHit.position;
            }
            else
            {
                destination = Vector3.zero;
            }

            return destination;
        }

        public static Object[] getRPCObject(int i, bool isTrue = true, int rangeMin = 0, int rangeMax = 0, bool rangeFirst = false, bool isPosition = false, Vector3 pos = new Vector3())
        {
            Debug.Msg("getRPCObject", 3);
            Object[] obj = new Object[i];
            if (i > 0)
            {
                Boolean boolean = default(Boolean);
                if (!rangeFirst)
                {
                    if (isTrue)
                        boolean.m_value = true;
                    else
                        boolean.m_value = false;
                    obj[0] = boolean.BoxIl2CppObject();

                    if (i == 2)
                    {
                        Int32 integer = default(Int32);
                        integer.m_value = Random.Range(rangeMin, rangeMax);
                        obj[1] = integer.BoxIl2CppObject();
                    }
                }
                else
                {
                    Int32 integer = default(Int32);
                    integer.m_value = Random.Range(rangeMin, rangeMax);
                    obj[0] = integer.BoxIl2CppObject();
                }
            }
            if (isPosition)
            {
                Vector3 vector = default(Vector3);
                vector = pos;
                obj[0] = vector.BoxIl2CppObject();
            }

            return obj;
        }
        public static Object[] getRPCObjectEmf(int i, Vector3 pos, int type = 0)
        {
            Debug.Msg("getRPCObjectEmf", 3);
            Object[] obj = new Object[i];
            if (i > 0)
            {
                Vector3 vector = default(Vector3);
                Int32 integer = default(Int32);
                vector = pos;
                integer.m_value = type;

                obj[0] = vector.BoxIl2CppObject();
                obj[1] = integer.BoxIl2CppObject();
            }
            return obj;
        }
    }
}