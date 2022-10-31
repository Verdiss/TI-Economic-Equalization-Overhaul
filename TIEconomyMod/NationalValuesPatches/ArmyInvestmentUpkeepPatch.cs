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

            float baseCost = TemplateManager.global.nationalInvestmentArmyFactorHome; //Expected to be 1
            if (!__instance.useHomeInvestmentFactor)
            {
                baseCost = TemplateManager.global.nationalInvestmentArmyFactorAway; //Expected to be 2
            }


            __result = baseCost * GetMilTechArmyUpkeepMult(__instance.homeNation); //Multiply by upkeep multiplier from tech level


            return false; //Skip default getter
        }

        /// <summary>
        /// Returns the upkeep cost multiplier for armies of the input nation based on its military tech level
        /// </summary>
        /// <param name="nation"></param>
        /// <returns></returns>
        public static float GetMilTechArmyUpkeepMult(TINationState nation)
        {
            return Mathf.Max(1f, 1f + (2f * (nation.militaryTechLevel - 3f))); //Army costs 200% extra (additive) per tech level above 3. If tech is 3 or below, factor is clamped to 1.
        }
    }
}
