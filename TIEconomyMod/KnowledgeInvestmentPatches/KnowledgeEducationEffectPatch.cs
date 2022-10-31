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
    [HarmonyPatch(typeof(TINationState), "knowledgePriorityEducationChange", MethodType.Getter)]
    public static class KnowledgeEducationEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetKnowledgePriorityEducationChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the education effect of a knowledge investment to scale inversely with population size, and be scaled with an exponential decay based on current education
            //This keeps the education improvement rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an education gain rate of 0.05 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 166667 / population education gain

            float baseChange = 166667f / __instance.population;


            //Additionally, scale the education change based on current education, using an exponential decay relationship
            //With a multiplier of 4, and a base of 0.87, we get:
            //400% education gain at 0 education, 200% at 5 education, 100% at 10 education, 50% at 15 education, etc...
            float exDecayMult = 4f * Mathf.Pow(0.87f, __instance.education);


            __result = baseChange * exDecayMult;

            return false; //Skip original getter
        }
    }
}
