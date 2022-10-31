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
    [HarmonyPatch(typeof(TINationState), "economyScore", MethodType.Getter)]
    public static class BaseInvestmentPointPatch
    {
        [HarmonyPrefix]
        public static bool GetEconomyScoreOverwrite(ref float __result, TINationState __instance)
        {
            //Patches the amount of Investment Points available to a nation

            float baseIP = (float)(__instance.GDP / 100000000000.0); //1 IP per 100 billion GDP baseline

            float GDPPerCapitaMult = Mathf.Min(1f, 0.7f + (0.3f * __instance.perCapitaGDP / 15000f)); //Penalty to IP based on GDP per capita below 15k. 70% multiplier at 0 gdp per capita, 100% multiplier at or above 15k gdp per capita

            __result = baseIP * GDPPerCapitaMult;

            return false; //Skip original getter
        }
    }
}
