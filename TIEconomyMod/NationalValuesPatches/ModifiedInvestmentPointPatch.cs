using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod.InvestmentPointPatches
{
    [HarmonyPatch(typeof(TINationState), "modifiedEconomyScore", MethodType.Getter)]
    public static class ModifiedInvestmentPointPatch
    {
        [HarmonyPrefix]
        public static bool GetModifiedEconomyScoreOverwrite(ref float __result, TINationState __instance)
        {
            //Patches the modified investment point amount a nation has, which includes the bonus for advising councilors
            //Note that modifications due to unrest and military happen later in vanilla code, and are thus untouched here

            float adminBonus = (1f + __instance.adviserAdministrationBonus);
            __result = __instance.economyScore * adminBonus;

            return false; //Skip original getter
        }
    }
}
