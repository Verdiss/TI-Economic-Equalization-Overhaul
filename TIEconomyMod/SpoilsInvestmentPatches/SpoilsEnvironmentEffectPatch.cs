using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod.SpoilsInvestmentPatches
{
    [HarmonyPatch(typeof(TIGlobalValuesState), "AddSpoilsPriorityEnvEffect")]
    public static class SpoilsEnvironmentEffectPatch
    {
        [HarmonyPrefix]
        public static bool AddSpoilsPriorityEnvEffectOverwrite(TINationState nation, TIGlobalValuesState __instance)
        {
            //Overwrites the vanilla greenhouse gas emissions values for completing a spoils investment
            //Removes the scaling to emissions added in the vanilla function that does not work with this mod
            //Applies a diminishing returns effect on the emissions from resource regions

            //CO2
            float baseCO2 = TemplateManager.global.SpoCO2_ppm; //Flat per investment completion
            
            float resRegionCO2Mult = 1.0f;
            if (nation.currentResourceRegions >= 1) resRegionCO2Mult += 0.80f;
            if (nation.currentResourceRegions >= 2) resRegionCO2Mult += 0.80f / 2f;
            if (nation.currentResourceRegions >= 3) resRegionCO2Mult += 0.80f / 4f;
            if (nation.currentResourceRegions >= 4) resRegionCO2Mult += (0.80f / 8f) * (nation.currentResourceRegions - 3);
            //This is a stronger increase to emissions compared to the increase in money

            __instance.AddCO2_ppm(baseCO2 * resRegionCO2Mult);


            //CH4
            float baseCH4 = TemplateManager.global.SpoCH4_ppm;

            float resRegionCH4Mult = 1.0f;
            if (nation.currentResourceRegions >= 1) resRegionCH4Mult += 1.0f;
            if (nation.currentResourceRegions >= 2) resRegionCH4Mult += 1.0f / 2f;
            if (nation.currentResourceRegions >= 3) resRegionCH4Mult += 1.0f / 4f;
            if (nation.currentResourceRegions >= 4) resRegionCH4Mult += (1.0f / 8f) * (nation.currentResourceRegions - 3);
            //This is double the increase to emissions compared to the increase in money

            __instance.AddCH4_ppm(baseCH4 * resRegionCH4Mult);


            //N2O
            float baseN2O = TemplateManager.global.SpoN2O_ppm;

            float resRegionN2OMult = 1.0f;
            if (nation.currentResourceRegions >= 1) resRegionN2OMult += 1.0f;
            if (nation.currentResourceRegions >= 2) resRegionN2OMult += 1.0f / 2f;
            if (nation.currentResourceRegions >= 3) resRegionN2OMult += 1.0f / 4f;
            if (nation.currentResourceRegions >= 4) resRegionN2OMult += (1.0f / 8f) * (nation.currentResourceRegions - 3);
            //This is double the increase to emissions compared to the increase in money

            __instance.AddN2O_ppm(baseN2O * resRegionN2OMult);




            return false; //Skip the original method
        }
    }
}
