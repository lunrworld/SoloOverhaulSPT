using EFT.UI;
using HarmonyLib;
using SoloOverhaulforSPT;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SoloOverhaulforSPT.Patches
{
    internal class RemoveFleaMarketButton : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(MenuTaskBar).GetMethod("Awake", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(MenuTaskBar __instance)
        {
            var field = typeof(MenuTaskBar).GetField("_toggleButtons", BindingFlags.NonPublic | BindingFlags.Instance);
            var toggleButtons = (Dictionary<EMenuType, AnimatedToggle>)field.GetValue(__instance);
            toggleButtons[EMenuType.RagFair].gameObject.SetActive(false);
        }

    }
}
