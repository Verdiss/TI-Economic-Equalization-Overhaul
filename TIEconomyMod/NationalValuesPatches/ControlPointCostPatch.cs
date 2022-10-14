using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                float totalControlCost = __instance.economyScore / 10f; //Total cost to control the entire nation. 1 cost per 10 IP
                __result = totalControlCost / __instance.numControlPoints; //Total cost is split across the control points
            }
        }
    }
}
