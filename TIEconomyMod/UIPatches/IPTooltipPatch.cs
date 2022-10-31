using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIEconomyMod.InvestmentPointPatches;
using UnityEngine;

namespace TIEconomyMod.UIPatches
{
    [HarmonyPatch(typeof(NationInfoController), "BuildInvestmentTooltip")]
    public static class IPTooltipPatch
    {
        [HarmonyPrefix]
        public static bool BuildInvestmentTooltipOverwrite(ref string __result, TINationState nation)
        {
            //As vanilla
            StringBuilder stringBuilder = new StringBuilder(Loc.T("UI.Nation.InvestmentPoints")).AppendLine();
            float economyScore = nation.economyScore;
            stringBuilder.Append(Loc.T("UI.Nation.BaseIPs", economyScore.ToString("N2")));
            float num = nation.BaseInvestmentPoints_month();
            if (num != economyScore)
            {
                stringBuilder.Append(Loc.T("UI.Nation.CurrentIPs", num.ToString("N2")));
            }
            stringBuilder.AppendLine();
            float adviserAdministrationBonus = nation.adviserAdministrationBonus;
            if (adviserAdministrationBonus > 0f)
            {
                stringBuilder.AppendLine().AppendLine(Loc.T("UI.Nation.AdviserBonus", adviserAdministrationBonus.ToPercent("P0")));
            }
            float investmentPoints_unrestPenalty_frac = nation.investmentPoints_unrestPenalty_frac;
            if (investmentPoints_unrestPenalty_frac > 0f)
            {
                stringBuilder.AppendLine().AppendLine(Loc.T("UI.Nation.IPUnrestPenalty", investmentPoints_unrestPenalty_frac.ToPercent("P0")));
            }

            //Changed to reflect impact from mil tech
            int armiesAtHome = nation.armiesAtHome;
            if (armiesAtHome > 0)
            {
                stringBuilder.AppendLine().AppendLine(Loc.T("UI.Nation.HomeArmiesPenalty", TIUtilities.FormatSmallNumber(TemplateManager.global.nationalInvestmentArmyFactorHome * ArmyInvestmentUpkeepPatch.GetMilTechArmyUpkeepMult(nation)), TIUtilities.FormatSmallNumber((float)armiesAtHome * TemplateManager.global.nationalInvestmentArmyFactorHome)));
            }
            int deployedArmies = nation.deployedArmies;
            if (deployedArmies > 0)
            {
                stringBuilder.AppendLine().AppendLine(Loc.T("UI.Nation.AwayArmiesPenalty", TIUtilities.FormatSmallNumber(TemplateManager.global.nationalInvestmentArmyFactorAway * ArmyInvestmentUpkeepPatch.GetMilTechArmyUpkeepMult(nation)), TIUtilities.FormatSmallNumber((float)deployedArmies * TemplateManager.global.nationalInvestmentArmyFactorAway)));
            }

            //As vanilla
            int numNavies = nation.numNavies;
            if (numNavies > 0)
            {
                stringBuilder.AppendLine().AppendLine(Loc.T("UI.Nation.NaviesPenalty", TIUtilities.FormatSmallNumber(TemplateManager.global.nationalInvestmentNavyFactor), TIUtilities.FormatSmallNumber((float)numNavies * TemplateManager.global.nationalInvestmentNavyFactor)));
            }
            __result = stringBuilder.ToString().Trim();

            return false; //Skip original method
        }
    }
}
