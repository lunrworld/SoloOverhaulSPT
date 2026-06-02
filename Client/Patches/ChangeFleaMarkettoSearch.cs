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
using TMPro;
using UnityEngine;

namespace SoloOverhaulforSPT.Patches
{
    internal class ChangeFleaMarkettoSearch : ModulePatch
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

            if (toggleButtons.TryGetValue(EMenuType.RagFair, out AnimatedToggle fleaButton) && fleaButton != null)
            {
                Transform textTransform = fleaButton.transform.Find("Text");
                textTransform.gameObject.SetActive(true);

                if (textTransform != null)
                {
                    if (textTransform.TryGetComponent(out LocalizedText localizer))
                    {
                        localizer.enabled = false;
                    }

                    if (textTransform.TryGetComponent(out TextMeshProUGUI textMesh))
                    {
                        textMesh.text = "SEARCH";
                    }
                }
            }
        }

    }
}
