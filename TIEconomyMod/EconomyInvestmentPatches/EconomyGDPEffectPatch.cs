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
    [HarmonyPatch(typeof(TINationState), "economyPriorityPerCapitaIncomeChange", MethodType.Getter)]
    public static class EconomyGDPEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetEconomyPriorityPerCapitaIncomeChangeOverwrite(ref float __result, TINationState __instance)
        {
            //This patch changes the economy investment's GDP effect from primarily a per capita GDP effect to a primarily national GDP effect, which is then distributed per capita
            //The end goal is for a country to achieve roughly 1-2% GDP growth every month, after modifiers, when at 100% econony investment priority
            
            //A economy investment costs 1 IP, and an IP captures 10 Bn GDP-months, so it represents 10 Bn GDP-months
            //Thus if about 0.1-0.2 Bn GDP is gained every economy investment completion, the growth of GDP will be the desired 1-2% monthly at 100% economy priority
            //Orgs, Habs, and Tech will increase this amount, so a growth rate of 3-4% monthly is likley attainable at endgame, allowing for massive economic shifts.
            //Keep in mind that most of the time 100% economy investment is not viable, so actual growth rates will be much smaller barring an extreme push
            
            //Example: A country has 1,000 Bn GDP and 20 Mn population. It thus has 100 IP. It can complete 100 economy investments a month.
            //Each investment gives 0.2 Bn GDP after all calculations, so over the course of the month it gains 100 * 0.2 Bn = 20 Bn GDP, 2% of its starting GDP.
            //Its population is 20 million, so the per capita GDP increase is $1000 over the course of the month.

            //Example 2: The country is one one-hundredth the size, with equal other stats. It has 10 Bn GDP and 1 IP a month.
            //It can complete 1 economy investment a month at 100% priority. That gives 0.2 Bn GDP, increasing its GDP by 2%.
            //Its 0.2 million population also gain the same $1000 per capita GDP, though all at once as opposed to in 100 $10 divisions.

            float baseGDPChange = 100000000; //One investment gives 0.10 Bn GDP -- this gives approx 1.0% monthly economic growth if at 100% economic investment priority
            float resourceRegionsBonus = __instance.currentResourceRegions * 10000000; //Plus 0.01 Bn per resource region
            float coreEcoRegionsBonus = __instance.currentCoreEconomicRegions * 10000000; //Plus 0.01 Bn per core economic region
            //TODO consider whether the way resource and economic regions scale economic growth relative to country size is appropriate

            float summedGDPChange = baseGDPChange + resourceRegionsBonus + coreEcoRegionsBonus;


            float democracyMult = 0.8f + (__instance.democracy * (0.4f / 10f)); //get 80% of the gdp at 0 democracy, 120% at 10 democracy
            float cohesionMult = 1.1f - (Mathf.Abs(__instance.cohesion - 5f) * (0.2f / 5f)); //get 90% of gdp at 0 or 10 cohesion, 110% at 5 cohesion
            float educationMult = 0.5f + (__instance.education * (1f / 10f)); //get 50% of the gdp at 0 education, 150% at 10 education, and even more at higher education values

            float perCapGDPMult = 4f - Mathf.Min(3f, __instance.perCapitaGDP * (3f / 15000f)); //Poor nation catch-up: Up to a 400% gdp gain modifier at close to 0 gdp per capita, returning to 100% at 15k gdp per capita and staying at 100% for higher values

            float finalGDPChange = summedGDPChange * democracyMult * cohesionMult * educationMult * perCapGDPMult; //Final value for overall country GDP change taking all modifiers into account
            

            float finalPerCapGDPChange = finalGDPChange / __instance.population; //Amount to change GDP per capita by

            __result = finalPerCapGDPChange;

            return false; //Skip original getter
        }
    }
}
