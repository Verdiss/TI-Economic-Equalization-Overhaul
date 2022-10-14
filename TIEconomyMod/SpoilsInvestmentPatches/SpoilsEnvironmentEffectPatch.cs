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
            //Necessary due to the scaling to emissions added in the vanilla function that does not work with this mod

            float baseCO2 = TemplateManager.global.SpoCO2_ppm; //Flat per investment completion
            float resRegionCO2 = TemplateManager.global.SpoResCO2_ppm; //Added per resource region
            float summedCO2 = baseCO2 + (resRegionCO2 * (float)nation.currentResourceRegions);
            __instance.AddCO2_ppm(summedCO2);

            float baseCH4 = TemplateManager.global.SpoCH4_ppm;
            float resRegionCH4 = TemplateManager.global.SpoResCH4_ppm;
            float summedCH4 = baseCH4 + (resRegionCH4 * (float)nation.currentResourceRegions);
            __instance.AddCH4_ppm(summedCH4);

            float baseN2O = TemplateManager.global.SpoN2O_ppm;
            float resRegionN2O = TemplateManager.global.SpoResN2O_ppm;
            float summedN2O = baseN2O + (resRegionN2O * nation.currentResourceRegions);
            __instance.AddN2O_ppm(summedN2O);

            return false; //Skip the original method
        }
    }
}
