using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "economyPriorityInequalityChange", MethodType.Getter)]
    public static class EconomyInequalityEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetEconomyPriorityInequalityChangeOverwrite(ref float __result, TINationState __instance)
        {
            //This patch changes the amount of inequality gained from the economy investment to scale based on country population size
            //In other words, given a constant GDP per capita, a country should receive the same monthly impact to inequality at a given economic investment priority regardless of its population
            //This means the amount of inequality gained from a single investment must be in an inverse relationship with the country's population

            //The goal is to have the effect of an economy investment be  around 1/10th to 1/15th the strength of a welfare investment, as in vanilla
            //This means the 333333 / population inequality change for welfare becomes 33333 / population to 22222 / population for economy, I settled on 25000
            //See the WelfareInequalityEffectPatch for a full explanation on how this number is derived.

            float baseInequalityGain = 25000f / __instance.population; //About 14 times weaker than welfare by default
            float resourceRegionsMult = 1f + (0.1f * __instance.currentResourceRegions); //10% increase to inequality gain per resource region -- note that as is, a resource region gives a better than 10% increase to GDP growth

            __result = baseInequalityGain * resourceRegionsMult;

            return false; //Skip original getter
        }
    }
}
