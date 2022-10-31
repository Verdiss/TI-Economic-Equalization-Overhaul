using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TIEconomyMod.UnityInvestmentPatches
{
    [HarmonyPatch(typeof(TINationState), "unityPriorityDemocracyChange", MethodType.Getter)]
    public static class UnityDemocracyEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetUnityPriorityDemocracyChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the democracy effect of a unity investment to scale inversely with population size
            //This keeps the democracy reduction rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //Goal is 0.010 monthly democracy reduction at 100% unity
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 33333 / population democracy change

            __result = -33333f / __instance.population;

            return false; //Skip original getter
        }
    }
}
