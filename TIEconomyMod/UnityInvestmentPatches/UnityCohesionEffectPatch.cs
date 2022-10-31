using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TIEconomyMod.UnityInvestmentPatches
{
    [HarmonyPatch(typeof(TINationState), "unityPriorityCohesionChange", MethodType.Getter)]
    public static class UnityCohesionEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetUnityPriorityCohesionChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the cohesion effect of a unity investment to scale inversely with population size
            //This keeps the cohesion gain rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //Goal is 1.0 monthly cohesion gain at 100% unity and 0 education, with education scaling down to at worst half that afterwards as in vanilla
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 3333333 / population cohesion change

            float cohesionGain = 3333333f / __instance.population;

            float cohesionEducationMult = 1f - Mathf.Min(0.5f, (0.5f * __instance.education / 10f)); //100% mult at 0 education, at worst 50% mult at or above 10 education

            __result = cohesionGain * cohesionEducationMult;

            return false; //Skip original getter
        }
    }
}
