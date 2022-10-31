using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod.SpoilsInvestmentPatches
{
    [HarmonyPatch(typeof(TINationState), "spoilsPriorityMoney", MethodType.Getter)]
    public static class SpoilsMoneyEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetSpoilsPriorityMoneyOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the instant money effect of a spoils investment to be a flat value, not scaled by nation size

            float baseMoneyGained = 240f; //Base 240 money

            //Add money per resource region, with diminishing returns
            float resourceRegionBonusMoney = 0f;
            if (__instance.currentResourceRegions >= 1) resourceRegionBonusMoney += 80f; //80 extra for the 1st region
            if (__instance.currentResourceRegions >= 2) resourceRegionBonusMoney += 40f; //40 extra for the 2nd
            if (__instance.currentResourceRegions >= 3) resourceRegionBonusMoney += 20f; //20 extra for the 3rd
            if (__instance.currentResourceRegions >= 4) resourceRegionBonusMoney += 10f * (__instance.currentResourceRegions - 3); //10 extra for the 4th and on


            //Up to 30% extra money at 0 democracy, 0% extra at 10 democracy
            float democracyMult = 1.3f - (__instance.democracy * (0.3f / 10f));

            __result = (baseMoneyGained + resourceRegionBonusMoney) * democracyMult;


            return false; //Skip original getter
        }
    }
}
