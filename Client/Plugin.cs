using BepInEx;
using BepInEx.Logging;
using SoloOverhaulforSPT.Patches;

namespace SoloOverhaulforSPT
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("com.lunarworld.solooverhaul", "Solo Overhaul", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            new DisableInsuranceItem().Enable();
            new DisableInsuranceItemClass().Enable();
            new DisableInsuranceScreen().Enable();
            new RemoveFleaMarketButton().Enable();
            LogSource = Logger;
            LogSource.LogInfo("SOH loaded!");
        }
    }
}