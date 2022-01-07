using UnityEngine;
using MelonLoader;

namespace C4PhasMod {
    class Fullbright {
        public static void Enable() {
            if (Main.boneTransform != null && Main.initializedScene > 1 && isAlreadyOn == false) {
                isAlreadyOn = true;
                Debug.Msg("isAlreadyOn: true", 3);
                Main.light = Main.boneTransform.GetComponent<Light>();
                Object.Destroy(Main.boneTransform.GetComponent<Light>());
                Main.light = Main.boneTransform.gameObject.AddComponent<Light>();
                Main.light.color = Color.white;
                Main.light.type = LightType.Spot;
                Main.light.shadows = LightShadows.None;
                Main.light.range = 99f;
                Main.light.spotAngle = 9999f;
                Main.light.intensity = 0.3f;
                Debug.Msg("boneTransform set", 3);
            } else if (isAlreadyOn == false) {
                CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                Debug.Msg("No boneTransform! Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"), 1);
            }
        }

        public static void Disable() {
            isAlreadyOn = false;
            Debug.Msg("isAlreadyOn: false", 3);
            Object.Destroy(Main.light);
        }

        private static bool isAlreadyOn = false;
    }
}
