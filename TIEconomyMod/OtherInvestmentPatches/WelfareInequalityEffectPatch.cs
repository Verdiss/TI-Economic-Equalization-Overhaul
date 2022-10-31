using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "welfarePriorityInequalityChange", MethodType.Getter)]
    public static class WelfareInequalityEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetWelfarePriorityInequalityChangeOverwrite(ref float __result, TINationState __instance)
        {
            //This patch changes the amount of inequality removed by the welfare investment to scale based on country population size
            //In other words, given a constant GDP per capita, a country should receive the same monthly reduction to inequality at a given welfare investment priority regardless of its population
            //This means the amount of inequality lost from a single investment must be in an inverse relationship with the country's population
            //The goal is a 30k GDP per capita nation gets 0.10 monthly inequality reduction at 100% priority

            //IP = GDP / 100,000,000,000 = perCapGDP * population / 100,000,000,000
            //A country completes IP welfare investments a month at 100% welfare priority
            //Thus monthlyInequalityChange = singleInvestChange * IP = singleInvestChange * perCapGDP * population / 100,000,000,000
            //Fixing perCapGDP at 30,000, and monthlyInequalityChange at 0.05 based on our goal, we get:
            //singleInvestChange = 0.10 / (30,000 * population / 100,000,000,000) = (0.10 * 3333333) / population = 333333 / population
            //So a single welfare investment should decrease the country's inequality by 333333 / population

            //Example: A country has 600 Bn GDP and 20 Mn population. It thus has 6 IP. It can complete 6 welfare investments a month.
            //Each investment should decrease inequality by 333333/20,000,000 = 0.0167
            //6 investments a month thus reduces inequality by 0.10, the desired amount, as the GDP per capita is 30k

            //Example2: The country's GDP is doubled, so its per-capita GDP is 60k instead of 30k. It has 1,200 Bn GDP, for 12 IP.
            //Each investment still removes 0.0167 inequality, as this is a function only of population which is still 20 million.
            //12 investments a month removes 0.20 inequality a month, twice the amount due to twice the economic expense for the same population.

            __result = -333333f / __instance.population;

            return false; //Skip original getter
        }
    }
}
