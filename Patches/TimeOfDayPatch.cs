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
            Debug.Log($"Hour changed, Lung Docked: {Plugin.IsLungDocked}, Rad Level: {Plugin.CurrentRadiationLevel}");
            if (GameNetworkManager.Instance == null || Plugin.IsLungDocked)
                return;

            if (!GameNetworkManager.Instance.localPlayerController.isPlayerDead &&
                GameNetworkManager.Instance.localPlayerController.isPlayerControlled &&
                GameNetworkManager.Instance.localPlayerController.isInsideFactory &&
                !Plugin.IsLungDocked)
                    GameNetworkManager.Instance.localPlayerController.DamagePlayer(10 * Plugin.CurrentRadiationLevel, false);

            Plugin.CurrentRadiationLevel++;
        }
    }
}
