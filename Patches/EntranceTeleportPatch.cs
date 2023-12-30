using HarmonyLib;
using UnityEngine;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(EntranceTeleport))]
    internal class EntranceTeleportPatch
    {
        private static bool radiationPresentLastCheck;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        private static void UpdatePatch(ref bool ___enemyNearLastCheck, ref bool ___isEntranceToBuilding, ref InteractTrigger ___triggerScript)
        {
            if (___triggerScript == null || !___isEntranceToBuilding)
                return;

            if (!Plugin.IsLungDocked && !___enemyNearLastCheck && !radiationPresentLastCheck)
            {
                radiationPresentLastCheck = true;
                ___triggerScript.hoverTip = "[Radiation detected!]";
            }
            else
            {
                radiationPresentLastCheck = false;
                ___triggerScript.hoverTip = "Enter: [LMB]";
            }
        }
    }
}
