using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "spaceFundingPriorityIncomeChange", MethodType.Getter)]
    public static class FundingEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetSpaceFundingPriorityIncomeChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patches the monthly funding gain from completing a funding investment
            
            //A vanilla country with 10 IP can complete 10 funding investments a month
            //Each gives 7 funding and thus in total yearly funding income increases by 70 a month
            //This is at the cost of 30 control cap, so the funding increase to control cost ratio is about 2 for this middle-range country

            //A patched country gets 10 IP per control cost.
            //To keep the same ratio of funding gain to control cost, it is thus necessary to change the reward to be 0.2 funding gained
            //Changed to 0.3 for better balance

            __result = 0.3f;

            return false; //Skip original getter
        }
    }
}
