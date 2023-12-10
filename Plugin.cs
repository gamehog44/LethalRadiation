using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalRadiation.Patches;

namespace LethalRadiation
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static BaseUnityPlugin Instance { get; private set; }
        public static ManualLogSource PluginLogger { get; private set; }
        public static int CurrentRadiationLevel = 1;
        public static bool IsLungDocked = true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            harmony.PatchAll(typeof(ElevatorAnimationEventsPatch));
            harmony.PatchAll(typeof(EntranceTeleportPatch));
            harmony.PatchAll(typeof(LungPropPatch));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(TimeOfDayPatch));

            PluginLogger = Logger;
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
