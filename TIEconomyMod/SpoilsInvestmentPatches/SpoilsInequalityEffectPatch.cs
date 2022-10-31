using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod.SpoilsInvestmentPatches
{
    [HarmonyPatch(typeof(TINationState), "spoilsPriorityInequalityChange", MethodType.Getter)]
    public static class SpoilsInequalityEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetSpoilsPriorityInequalityChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the inequality effect of a spoils investment to scale inversely with population size
            //This keeps the inequality gain rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an inequality gain rate of 0.05 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 166667 / population democracy change

            float inequalityGain = 166667f / __instance.population;
            float inequalityResourceRegionMult = 1f + (0.2f * __instance.currentResourceRegions); //20% more inequality per resource region

            __result = inequalityGain * inequalityResourceRegionMult;

            return false; //Skip original getter
        }
    }
}
