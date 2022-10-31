using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TIEconomyMod.SpoilsInvestmentPatches
{
    [HarmonyPatch(typeof(TINationState), "spoilsPriorityDemocracyChange", MethodType.Getter)]
    public static class SpoilsDemocracyEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetSpoilsPriorityDemocracyChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the democracy effect of a spoils investment to scale inversely with population size
            //This keeps the democracy reduction rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an democracy reduction rate of 0.02 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 66667 / population democracy change

            __result = -66667f / __instance.population;

            return false; //Skip original getter
        }
    }
}
