using GameNetcodeStuff;
using HarmonyLib;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void Update(ref PlayerControllerB __instance)
        {
            if (!__instance.isPlayerDead && __instance.isPlayerControlled && !Plugin.IsLungDocked && __instance.isInsideFactory)
                __instance.drunkness = Plugin.CurrentBlurAmount;
            else
                __instance.drunkness = 0.0f;
        }
    }
}
