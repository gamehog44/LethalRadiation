using HarmonyLib;
using UnityEngine;

namespace LethalRadiation.Patches
{
    [HarmonyPatch(typeof(ElevatorAnimationEvents))]
    internal class ElevatorAnimationEventsPatch
    {
        [HarmonyPatch("ElevatorFullyRunning")]
        [HarmonyPostfix]
        private static void ElevatorFullyRunningPatch()
        {
            Debug.Log("Ship Leaving");
            Plugin.IsLungDocked = true;
            Plugin.CurrentRadiationLevel = 1;
        }
    }
}
