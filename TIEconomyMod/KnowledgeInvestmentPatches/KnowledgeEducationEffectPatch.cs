using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "knowledgePriorityEducationChange", MethodType.Getter)]
    public static class KnowledgeEducationEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetKnowledgePriorityEducationChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the education effect of a knowledge investment to scale inversely with population size
            //This keeps the education improvement rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an education gain rate of 0.025 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 8333 / population education gain

            __result = 8333 / __instance.population;

            return false; //Skip original getter
        }
    }
}
