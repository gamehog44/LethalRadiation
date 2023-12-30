using HarmonyLib;
using UnityEngine;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        [HarmonyPatch("ResetEnemySpawningVariables")]
        [HarmonyPrefix]
        private static void ResetEnemySpawningVariablesPatch()
        {
            Plugin.IsLungDocked = true;
            Plugin.CurrentDamageAmount = LRConfig.BaseDamage.Value;
            Plugin.CurrentBlurAmount = LRConfig.BaseBlur.Value;
            Plugin.CurrentHour = 0;

            Debug.Log($"LR variables reset! docked? {Plugin.IsLungDocked}");
        }
    }
}
