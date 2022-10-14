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
            //The goal is a 30k GDP per capita nation gets 0.05 monthly inequality reduction at 100% priority

            //IP = GDP / 10,000,000,000 = perCapGDP * population / 10,000,000,000
            //A country completes IP welfare investments a month at 100% welfare priority
            //Thus monthlyInequalityChange = singleInvestChange * IP = singleInvestChange * perCapGDP * population / 10,000,000,000
            //Fixing perCapGDP at 30,000, and monthlyInequalityChange at 0.05 based on our goal, we get:
            //singleInvestChange = 0.05 / (30,000 * population / 10,000,000,000) = (0.05 * 333333) / population = 167 / population
            //So a single welfare investment should decrease the country's inequality by 16667 / population

            //Example: A country has 600 Bn GDP and 20 Mn population. It thus has 60 IP. It can complete 60 welfare investments a month.
            //Each investment should decrease inequality by 16667/20,000,000 = 0.00083
            //60 investments a month thus reduces inequality by 0.0498, the desired amount, as the GDP per capita is 30k

            //Example2: The country's GDP is doubled, so its per-capita GDP is 60k instead of 30k. It has 1,200 Bn GDP, for 120 IP.
            //Each investment still removes 0.00083 inequality, as this is a function only of population which is still 20 million.
            //120 investments a month removes 0.0996 inequality a month, twice the amount due to twice the economic expense for the same population.

            __result = 16667 / __instance.population;

            return false; //Skip original getter
        }
    }
}
