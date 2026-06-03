using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using SoloOverhaulforSPT.Patches;

namespace SoloOverhaulforSPT
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("com.lunarworld.solooverhaul", "Solo Overhaul", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        private ConfigEntry<bool> DisableInsuranceEnabled;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            DisableInsuranceEnabled = Config.Bind("General", "Disable Insurance", true, "Disables insurance and pre-raid screen (REQUIRES RESTART)");
            if (DisableInsuranceEnabled.Value)
            {
                new DisableInsuranceItem().Enable();
                new DisableInsuranceItemClass().Enable();
                new DisableInsuranceScreen().Enable();
            }
            new ChangeFleaMarkettoSearch().Enable();
            LogSource = Logger;
            LogSource.LogInfo("SOH loaded!");
        }
    }
}