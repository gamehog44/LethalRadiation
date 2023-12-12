using BepInEx;
using HarmonyLib;
using LethalRadiation.Patches;

namespace LethalRadiation
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public static BaseUnityPlugin Instance { get; private set; }

        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static int CurrentDamageAmount;
        public static float CurrentBlurAmount;
        public static bool IsLungDocked = true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            LRConfig.Setup();

            CurrentDamageAmount = LRConfig.BaseDamage.Value;
            CurrentBlurAmount = LRConfig.BaseBlur.Value;

            harmony.PatchAll(typeof(ElevatorAnimationEventsPatch));
            harmony.PatchAll(typeof(EntranceTeleportPatch));
            harmony.PatchAll(typeof(LungPropPatch));
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(TimeOfDayPatch));

            if (LRConfig.BlurEnabled.Value)
                harmony.PatchAll(typeof(PlayerControllerBPatch));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
