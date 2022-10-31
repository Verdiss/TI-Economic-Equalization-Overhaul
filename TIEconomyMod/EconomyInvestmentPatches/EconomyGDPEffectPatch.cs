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
            //This patch changes the economy investment's GDP effect from increasing gdp per capita by a flat(ish) amount, to increasing gdp by a flat(ish) amount and distributing that across the pop as gdp per capita
            //The most significant change is that GDP growth is dependent on an exponential decay function off of per capita GDP. This makes developing poor countries much more effective than developing rich ones, accounting for all factors

            float baseGDPChange = 330000000f; //One investment gives a base amount of 0.33 Bn GDP

            //Resource and core economic region multiplier
            //Get a diminishing-return % bonus to growth based on total number of these regions
            int totalRegions = __instance.currentResourceRegions + __instance.currentCoreEconomicRegions;
            float regionsMult = 1f;
            if (totalRegions >= 1) regionsMult += 0.2f; //20% bonus for first region
            if (totalRegions >= 2) regionsMult += 0.1f; //10% bonus for second region
            if (totalRegions >= 3) regionsMult += 0.05f; //5% bonus for third region
            if (totalRegions >= 4) regionsMult += 0.025f * (totalRegions - 3); //2.5% bonus for fourth region and beyond

            //Demographic stat bonuses
            float educationMult = 1f + (0.15f * __instance.education); //get 15% bonus growth per education point
            float democracyMult = 1f + (0.05f * __instance.democracy); //get 5% bonus growth per democracy point
            float cohesionMult = 1.2f - (Mathf.Abs(__instance.cohesion - 5) * 0.04f); //at 5 cohesion, get 120% growth, reduced by 4% per point away from 5

            //Per capita GDP multiplier
            //This is an exponential decay function that gives countries with very low GDP per capita a large (up to 6 times!) bonus to growth
            //This is 6 for a country with 0 gdp per capita, about 3.25 for a country with 15k, 1.76 for a country with 30k, 1 for a country with 44k, and 0.34 for a country with 70k
            //In other words, countries with low gdp per capita will grow their absolute gdp per capita much faster than a country with higher, with things really speeding up for the first 15-20k, and really slowing down after 50k gdp per capita
            float perCapGDPMult = 6f * Mathf.Pow(0.96f, __instance.perCapitaGDP / 1000f);


            float modifiedGDPChange = baseGDPChange * regionsMult * educationMult * democracyMult * cohesionMult * perCapGDPMult; //Final amount of gdp to gain

            float finalPerCapGDPChange = modifiedGDPChange / __instance.population; //Amount to change GDP per capita by

            __result = finalPerCapGDPChange;

            return false; //Skip original getter
        }
    }
}
