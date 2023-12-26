using HarmonyLib;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class StartMatchLeverPatch
    {
        [HarmonyPatch("PullLever")]
        [HarmonyPostfix]
        private static void PullLeverPatch()
        {
            Plugin.IsLungDocked = true;
            Plugin.CurrentDamageAmount = LRConfig.BaseDamage.Value;
            Plugin.CurrentBlurAmount = LRConfig.BaseBlur.Value;
            Plugin.CurrentHour = 0;
        }
    }
}
