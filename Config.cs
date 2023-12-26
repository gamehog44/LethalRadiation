using BepInEx.Configuration;

namespace LethalRadiation
{
    internal class LRConfig
    {
        //Damage
        public static ConfigEntry<bool> DamageEnabled;
        public static ConfigEntry<int> BaseDamage;
        public static ConfigEntry<int> DamageInterval;

        // Screen Blur
        public static ConfigEntry<bool> BlurEnabled;
        public static ConfigEntry<float> BaseBlur;
        public static ConfigEntry<float> BlurInterval;

        // Apparatus
        public static ConfigEntry<int> ApparatusValue;

        public static void Setup()
        {
            // Damage
            DamageEnabled = Plugin.Instance.Config.Bind("Damage", "Enable", true, "Determines if radiation should do damage to the player at the top of each hour");
            BaseDamage = Plugin.Instance.Config.Bind("Damage", "BaseAmount", 10, "The starting amount of damage radiation will do to players before it is increased");
            DamageInterval = Plugin.Instance.Config.Bind("Damage", "DamageIncreaseAmount", 1, "How much damage increases by at the top of each hour");

            // Screen Blur
            BlurEnabled = Plugin.Instance.Config.Bind("Screen Blur", "Enable", true, "Determine if radiation should cause a player's screen to get blurry while in the building");
            BaseBlur = Plugin.Instance.Config.Bind("Screen Blur", "BaseAmount", 0.02f, "The starting amount of screen blur radiation will cause before it is increased");
            BlurInterval = Plugin.Instance.Config.Bind("Screen Blur", "BlurIncreaseAmount", 0.02f, "How much screen blur gets worse by at the top of each hour");

            // Apparatus
            ApparatusValue = Plugin.Instance.Config.Bind("Apparatus", "Value", 80, "The scrap value of the apparatus");
        }
    }
}
