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
    [HarmonyPatch(typeof(TINationState), "knowledgePriorityCohesionChange", MethodType.Getter)]
    public static class KnowledgeCohesionEffectPatch
    {
        [HarmonyPrefix]
        public static bool GetKnowledgePriorityCohesionChangeOverwrite(ref float __result, TINationState __instance)
        {
            //Patch changes the cohesion effect of a knowledge investment to scale inversely with population size
            //This keeps the cohesion centering rate of nations of different populations but identical demographic stats otherwise the same

            //For a full explanation of the logic backing this change, see WelfareInequalityEffectPatch
            //I want an cohesion centering rate of 0.10 a month for a 30k GDP per capita nation
            //Using the same method as with the welfare inequality, this gives me a single investment effect of 333333 / population cohesion change

            //The cohesion change from knowledge is a centering effect, drawing cohesion to the median 5 rating, so some additonal logic is required

            float cohesionChangeAmount = Math.Min(Mathf.Abs(__instance.cohesion - 5f), (333333f / __instance.population)); //Calculate the amount of change and prevent overshooting 5
            if (__instance.cohesion > 5f)
            {
                cohesionChangeAmount *= -1f; //Make it reduce cohesion instead if it's currently above 5
            }

            __result = cohesionChangeAmount;

            return false; //Skip original getter
        }
    }
}
