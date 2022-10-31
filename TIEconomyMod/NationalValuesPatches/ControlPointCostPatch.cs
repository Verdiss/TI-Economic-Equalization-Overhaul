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
    /// <summary>
    /// Patches the getter for control cost of a single control point for a nation
    /// </summary>
    [HarmonyPatch(typeof(TINationState), "ControlPointMaintenanceCost", MethodType.Getter)]
    public static class ControlPointCostPatch
    {
        [HarmonyPostfix]
        public static void GetControlPointMaintenanceCostPostfix(ref float __result, TINationState __instance)
        {
            if (__result != 0) //Will be 0 and should stay 0 if the nation's controller is the aliens
            {
                float baseControlCost = __instance.economyScore; //Total cost to control the entire nation. 1 cost per 1 IP

                int numTechs = 0; //Number of control-cost-reducing techs that have been researched
                if (GameStateManager.GlobalResearch().finishedTechsNames.Contains("ArrivalInternationalRelations")) numTechs++;
                if (GameStateManager.GlobalResearch().finishedTechsNames.Contains("UnityMovements")) numTechs++;
                if (GameStateManager.GlobalResearch().finishedTechsNames.Contains("GreatNations")) numTechs++;
                if (GameStateManager.GlobalResearch().finishedTechsNames.Contains("ArrivalGovernance")) numTechs++;
                if (GameStateManager.GlobalResearch().finishedTechsNames.Contains("Accelerando")) numTechs++;

                float power = 1f; //Power to raise the base control cost to
                switch (numTechs)
                {
                    case 1:
                        power = 0.98f; //1500 cost nation -> 1296, 200 cost nation -> 180, 20 cost nation -> 19
                        break;
                    case 2:
                        power = 0.95f; //1500 cost nation -> 1041, 200 cost nation -> 153, 20 cost nation -> 17
                        break;
                    case 3:
                        power = 0.90f; //1500 cost nation -> 722, 200 cost nation -> 118, 20 cost nation -> 15
                        break;
                    case 4:
                        power = 0.85f; //1500 cost nation -> 501, 200 cost nation -> 90, 20 cost nation -> 13
                        break;
                    case 5:
                        power = 0.80f; //1500 cost nation -> 347, 200 cost nation -> 69, 20 cost nation -> 11
                        break;
                    default:
                        power = 1f; //No techs, keep baseline control cost
                        break;
                }

                __result = Mathf.Pow(baseControlCost, power) / __instance.numControlPoints; //Total cost is split across the control points
            }
        }
    }
}
