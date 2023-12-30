using BepInEx;
using HarmonyLib;
using LethalRadiation.Patches;
using UnityEngine;

namespace LethalRadiation
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public static BaseUnityPlugin Instance { get; private set; }

        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);

        public static int CurrentDamageAmount;
        public static float CurrentBlurAmount;
        public static int CurrentHour;
        public static bool IsLungDocked;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            LRConfig.Setup();

            CurrentDamageAmount = LRConfig.BaseDamage.Value;
            CurrentBlurAmount = LRConfig.BaseBlur.Value;
            CurrentHour = 0;
            IsLungDocked = true;

            harmony.PatchAll(typeof(EntranceTeleportPatch));
            harmony.PatchAll(typeof(LungPropPatch));
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(RoundManagerPatch));
            harmony.PatchAll(typeof(TimeOfDayPatch));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
