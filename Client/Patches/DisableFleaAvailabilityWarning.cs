using EFT.UI.Ragfair;
using SPT.Reflection.Patching;
using System;
using System.Reflection;

namespace SoloOverhaulforSPT.Patches
{
    internal class DisableFleaAvailabilityWarning : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(RagfairAvailabilityWarning).GetMethod("Show",
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new Type[] { typeof(RagFairClass) },
                null
            );
        }

        [PatchPrefix]
        protected static bool Prefix()
        {
            return false;
        }
    }

}