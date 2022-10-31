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
    [HarmonyPatch(typeof(TINationState), "militaryPriorityUnrestChange", MethodType.Getter)]
    public static class MilitaryUnrestEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetMilitaryPriorityUnrestChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the unrest reduction effect of a military investment to scale inversely with population size
            //This keeps the unrest reduction rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch

            //Vanilla sets the unrest reduction rate at 0.1 - democracy / 100 per completed military investment
            //I want to preserve this democracy impact, so the calculation must also take this into account
            //I say at 0 democracy, you get 100% of the unrest reduction, and at 10 democracy, you geto 0% -- same as vanilla

            //I want an unrest reduction rate of 0.67 a month for a 30k GDP per capita nation at 0 democracy
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 2222222 / population unrest reduction at 0 democracy

            float baseUnrestReduction = 2222222f / __instance.population;

            float democracyMult = (10f - __instance.democracy) / 10f; //0% at 10 democracy, 100% at 0

            float totalUnrestChange = 0f - Mathf.Min(__instance.unrest, baseUnrestReduction * democracyMult); //Make negative (as per vanilla) and prevent overshooting 0


            __result = totalUnrestChange;

            return false; //Skip original getter
        }
    }
}
