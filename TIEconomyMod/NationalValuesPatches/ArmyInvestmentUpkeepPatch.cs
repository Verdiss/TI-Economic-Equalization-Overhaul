using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TIEconomyMod.InvestmentPointPatches
{
    [HarmonyPatch(typeof(TIArmyState), "investmentArmyFactor", MethodType.Getter)]
    public static class ArmyInvestmentUpkeepPatch
    {
        [HarmonyPrefix]
        public static bool GetInvestmentArmyFactorOverwrite(ref float __result, TIArmyState __instance)
        {
            //Patch changes the IP upkeep of armies to be dependent on mil tech level of the owning nation
            //Armies cost 15 IP per tech level above 3.0, with armies at 3.0 or lower costing only 1 IP
            //Armies away from home or in combat cost double this amount

            float baseCost = TemplateManager.global.nationalInvestmentArmyFactorHome; //Expected to be 1
            if (!__instance.useHomeInvestmentFactor)
            {
                baseCost = TemplateManager.global.nationalInvestmentArmyFactorAway; //Expected to be 2
            }

            float techFactor = Mathf.Max(1f, 15f * (__instance.homeNation.militaryTechLevel - 3f)); //Army costs 15 times base per tech level above 3. If tech is 3 or below, factor is clamped to 1.

            __result = baseCost * techFactor;

            return false; //Skip default getter
        }
    }
}
