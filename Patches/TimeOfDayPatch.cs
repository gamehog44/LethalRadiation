using HarmonyLib;
using UnityEngine;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class TimeOfDayPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void UpdatePatch(TimeOfDay __instance)
        {
            if (GameNetworkManager.Instance == null || Plugin.IsLungDocked)
                return;

            var playerController = GameNetworkManager.Instance.localPlayerController;

            // Handle damage at the top of the hour
            if (__instance.hour != Plugin.CurrentHour)
            {
                Debug.Log($"Hour changed, hour: {__instance.hour}, currentHour: {Plugin.CurrentHour}, docked: {Plugin.IsLungDocked}, damage value: {LRConfig.DamageInterval.Value}, total damage: {Plugin.CurrentDamageAmount}, blur value: {LRConfig.BlurInterval.Value}, blur amount: {Plugin.CurrentBlurAmount}");

                if (LRConfig.DamageEnabled.Value && !playerController.isPlayerDead && playerController.isPlayerControlled && playerController.isInsideFactory)
                    playerController.DamagePlayer(Plugin.CurrentDamageAmount, false);

                Plugin.CurrentDamageAmount += LRConfig.DamageInterval.Value;
                Plugin.CurrentBlurAmount += LRConfig.BlurInterval.Value;
                Plugin.CurrentHour = __instance.hour;
            }

            // Handle screen blur
            if (LRConfig.BlurEnabled.Value && !playerController.isPlayerDead && playerController.isPlayerControlled && playerController.isInsideFactory && playerController.drunkness <= Plugin.CurrentBlurAmount)
                playerController.drunkness = Plugin.CurrentBlurAmount;
        }
    }
}
