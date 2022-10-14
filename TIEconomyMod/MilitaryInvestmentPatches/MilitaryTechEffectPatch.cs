using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "militaryPriorityTechLevelChange", MethodType.Getter)]
    public static class MilitaryTechEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetMilitaryPriorityTechLevelChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the military tech effect of a military investment to scale inversely with population size
            //This keeps the military tech improvement rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an military tech gain rate of 0.0125 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 4167 / population military tech gain

            float idealMilTechGain = 4167 / __instance.population;


            //For countries with >0 unrest, some proportion of the military investment will go to reducing that unrest instead of boosting mil tech
            //That value is tied to how quickly unrest is reducing, which is tied strongly to democracy but also capped at the current unrest to prevent passing 0
            //If the maximum amount of unrest is being removed, (due to the country being at low democracy), then no mil tech should be gained
            //If little unrest is being removed, due to either high democracy or the unrest being within 1 investment of 0, nearly full mil tech should be gained
            float expectedUnrestReduction = 111111 / __instance.population; //This is pulled from MilitaryUnrestEffectPatch. It is the amount of unrest a military investment would remove at 0 democracy and >> 0 unrest
            float unrestReductionRatio = Mathf.Abs(__instance.militaryPriorityUnrestChange / expectedUnrestReduction); //Fraction of the expected unrest reduction that will actually be removed

            //If unrestReductionRatio is 1, all focus is on removing unrest and 0% mil tech should be gained
            //If it is 0, all focus is on mil tech, and 100% of the ideal mil tech should be gained
            float unrestMult = 1f - unrestReductionRatio;

            __result = idealMilTechGain * unrestMult;

            return false; //Skip original getter
        }
    }
}
