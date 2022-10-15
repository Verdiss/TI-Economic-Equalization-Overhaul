using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIEconomyMod
{
    [HarmonyPatch(typeof(TINationState), "SpoilsPriorityComplete")]
    public static class SpoilsEffectsPatch
    {
        [HarmonyPrefix]
        public static bool SpoilsPriorityCompleteOverwrite(TINationState __instance)
        {
            //This patch changes the effects of the spoils investment
            //This overwrite is necessary to fix the propoganda effect, which would otherwise be far too powerful
            //Otherwise, this method is almost as vanilla, barring referenced values that are changed in other patches

            //Below as vanilla
            __instance.AddToInequality(__instance.spoilsPriorityInequalityChange);
            __instance.AddToDemocracy(__instance.spoilsPriorityDemocracyChange);

            TIFactionState controlPointTypeOwner = __instance.GetControlPointTypeOwner(ControlPointType.Aristocracy);
            TIFactionState controlPointTypeOwner2 = __instance.GetControlPointTypeOwner(ControlPointType.ExtractiveSector);
            foreach (TIControlPoint controlPoint in __instance.controlPoints)
            {
                float num = __instance.spoilsPriorityMoneyPerControlPoint * ((controlPointTypeOwner == controlPoint.faction) ? TemplateManager.global.aristoracySpoilsMult : 1f) + ((controlPointTypeOwner2 == controlPoint.faction) ? (TemplateManager.global.extractiveSpoilsBonusPerResourceRegion * (float)__instance.currentResourceRegions) : 0f);
                num += TIEffectsState.SumEffectsModifiers(Context.SpoilsOutput, controlPoint.faction, num);
                if (controlPoint.faction != null && !controlPoint.benefitsDisabled)
                {
                    controlPoint.faction.AddToCurrentResource(num, FactionResource.Money);
                    controlPoint.faction.thisWeeksCumulativeSpoils += num;
                }
            }

            //-------Propoganda Effect-------
            //I do not understand the PropogandOnPop call chain clearly enough to know how it scales with population, and thus how to make it scale how I want.
            //For now, the best way for me to handle this issue is to simply disable the system entirely, pending additional research and math
            //TODO look into the PropogandOnPop call chain in detail to understand how population size relates to ideology shifting
            //Dictionary<TIFactionState, int> dictionary = new Dictionary<TIFactionState, int>();
            //foreach (TIControlPoint controlPoint2 in __instance.controlPoints)
            //{
            //    if (controlPoint2.owned)
            //    {
            //        if (!dictionary.ContainsKey(controlPoint2.faction))
            //        {
            //            dictionary.Add(controlPoint2.faction, 1);
            //        }
            //        else
            //        {
            //            dictionary[controlPoint2.faction]++;
            //        }
            //    }
            //}
            //foreach (TIFactionState key in dictionary.Keys)
            //{
            //    float strength = (0f - __instance.education * (float)(dictionary[key] / __instance.numControlPoints)) / 4f;
            //    __instance.PropagandaOnPop(key.ideology, strength);
            //}


            //Below as vanilla
            TIGlobalValuesState.GlobalValues.AddSpoilsPriorityEnvEffect(__instance);


            return false; //Cancels the call to the original method but runs any other prefixes
        }
    }
}
