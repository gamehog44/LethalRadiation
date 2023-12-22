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

        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static int CurrentDamageAmount;
        public static float CurrentBlurAmount;
        public static int CurrentHour;
        public static bool IsLungDocked = true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            LRConfig.Setup();

            CurrentDamageAmount = LRConfig.BaseDamage.Value;
            CurrentBlurAmount = LRConfig.BaseBlur.Value;
            CurrentHour = 0;

            harmony.PatchAll(typeof(ElevatorAnimationEventsPatch));
            harmony.PatchAll(typeof(EntranceTeleportPatch));
            harmony.PatchAll(typeof(LungPropPatch));
            harmony.PatchAll(typeof(Plugin));

            if (LRConfig.BlurEnabled.Value)
                harmony.PatchAll(typeof(PlayerControllerBPatch));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void Update()
        {
            if (TimeOfDay.Instance == null || GameNetworkManager.Instance == null || IsLungDocked || TimeOfDay.Instance.hour == CurrentHour)
                return;

            Debug.Log($"Hour changed, hour: {TimeOfDay.Instance.hour}, currentHour: {CurrentHour}, docked: {IsLungDocked}, damage value: {LRConfig.DamageInterval.Value}, total damage: {CurrentDamageAmount}, blur value: {LRConfig.BlurInterval.Value}, blur amount: {CurrentBlurAmount}");

            CurrentHour = TimeOfDay.Instance.hour;

            if (LRConfig.DamageEnabled.Value &&
                GameNetworkManager.Instance != null &&
                !GameNetworkManager.Instance.localPlayerController.isPlayerDead &&
                GameNetworkManager.Instance.localPlayerController.isPlayerControlled &&
                GameNetworkManager.Instance.localPlayerController.isInsideFactory)
                GameNetworkManager.Instance.localPlayerController.DamagePlayer(CurrentDamageAmount, false);

            CurrentDamageAmount += LRConfig.DamageInterval.Value;
            CurrentBlurAmount += LRConfig.BlurInterval.Value;
        }

    }
}
