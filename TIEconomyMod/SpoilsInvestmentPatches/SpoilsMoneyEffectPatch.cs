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

            //A Vanilla nation with 10 IP can extract about 2000 money/month, at the cost of 30 control cap for 66 monthly money per control cost
            //To maintain balance, lets keep that monthly money to control cost rate the same, which means a spoils reward of about 66 money for 1 IP (which costs 1 control cost)
            float baseMoneyGained = 30f; //Base 30 money
            float resourceRegionBonusMoney = 3f * __instance.currentResourceRegions; //3 money per resource region
            float lowDemocracyBonusMoney = 1.5f * (10f - __instance.democracy); //2 money per democracy less than 10

            __result = baseMoneyGained + resourceRegionBonusMoney + lowDemocracyBonusMoney;


            return false; //Skip original getter
        }
    }
}
