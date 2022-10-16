using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "knowledgePriorityDemocracyChange", MethodType.Getter)]
    public static class KnowledgeDemocracyEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetKnowledgePriorityDemocracyChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the democracy effect of a knowledge investment to scale inversely with population size
            //This keeps the democracy improvement rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an democracy gain rate of 0.025 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 8333 / population democracy gain
            //After some testing, this was changed to 12000 / population

            __result = 12000f / __instance.population;

            return false; //Skip original getter
        }
    }
}
