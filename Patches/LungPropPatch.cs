using HarmonyLib;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(LungProp))]
    internal class LungPropPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartPatch(LungProp __instance)
        {
            if (LRConfig.OverrideApparatusValue.Value)
                __instance.scrapValue = LRConfig.ApparatusValue.Value;
        }

        [HarmonyPatch("EquipItem")]
        [HarmonyPrefix]
        private static void EquipItemPatch(ref bool ___isLungDocked)
        {
            if (___isLungDocked)
                Plugin.IsLungDocked = false;
        }
    }
}
