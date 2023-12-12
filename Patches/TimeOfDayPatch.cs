using HarmonyLib;
using UnityEngine;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class TimeOfDayPatch
    {
        [HarmonyPatch("OnHourChanged")]
        [HarmonyPostfix]
        private static void OnHourChangedPatch(int amount)
        {
            Debug.Log($"Hour changed, docked? {Plugin.IsLungDocked}");
            if (Plugin.IsLungDocked)
                return;

            if (LRConfig.DamageEnabled.Value &&
                GameNetworkManager.Instance != null &&
                !GameNetworkManager.Instance.localPlayerController.isPlayerDead &&
                GameNetworkManager.Instance.localPlayerController.isPlayerControlled &&
                GameNetworkManager.Instance.localPlayerController.isInsideFactory &&
                !Plugin.IsLungDocked)
                GameNetworkManager.Instance.localPlayerController.DamagePlayer(Plugin.CurrentDamageAmount, false);

            Plugin.CurrentDamageAmount += LRConfig.DamageInterval.Value;
            Plugin.CurrentBlurAmount += LRConfig.BlurInterval.Value;
        }
    }
}
