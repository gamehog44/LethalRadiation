using HarmonyLib;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(LungProp))]
    internal class LungPropPatch
    {

        [HarmonyPatch("EquipItem")]
        [HarmonyPrefix]
        private static void EquipItemPatch(ref bool ___isLungDocked)
        {
            if (___isLungDocked)
                Plugin.IsLungDocked = false;
        }
    }
}
