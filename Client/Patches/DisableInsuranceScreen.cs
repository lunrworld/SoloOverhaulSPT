using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SoloOverhaulforSPT.Patches
{
    internal class DisableInsuranceScreen : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(MainMenuControllerClass).GetMethod("method_80", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchTranspiler]
        protected static IEnumerable<CodeInstruction> PatchTranspiler(IEnumerable<CodeInstruction> originalInstructions)
        {
            MethodInfo showInsuranceScreenMethodInfo = typeof(MainMenuControllerClass).GetMethod("method_51", BindingFlags.Public | BindingFlags.Instance);
            MethodInfo showAcceptScreenMethodInfo = typeof(MainMenuControllerClass).GetMethod("method_52", BindingFlags.Public | BindingFlags.Instance);

            List<CodeInstruction> modifiedInstructions = originalInstructions.ToList();

            for (int i = 0; i < modifiedInstructions.Count; i++)    
            {
                if ((modifiedInstructions[i].opcode == OpCodes.Call) && ((MethodInfo)modifiedInstructions[i].operand == showInsuranceScreenMethodInfo))
                {
                    modifiedInstructions[i].operand = showAcceptScreenMethodInfo;
                }
            }

            return modifiedInstructions;
        }
    }
}
