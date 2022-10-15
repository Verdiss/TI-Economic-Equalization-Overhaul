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
    [HarmonyPatch(typeof(TINationState), "UnityPriorityComplete")]
    public static class UnityEffectsPatch
    {
        [HarmonyPrefix]
        public static bool UnityPriorityCompleteOverwrite(TINationState __instance)
        {
            //This patch changes the effects of the unity investment
            //This overwrite is necessary to fix the propoganda effect, which would otherwise be far too powerful
            //Otherwise, this method is almost as vanilla, barring referenced values that are changed in other patches


            //-------Propoganda Effect-------
            //As with the Spoils propoganda effect, it's unfortunately beyond me to understand this well enough to get things where I want them
            //For now, the best way for me to handle this issue is to simply disable the system entirely, pending additional research and math
            //TODO change this once I understand PropogandaOnPop
            //Dictionary<TIFactionState, int> dictionary = new Dictionary<TIFactionState, int>();
            //foreach (TIControlPoint controlPoint in __instance.controlPoints)
            //{
            //    if (controlPoint.owned)
            //    {
            //        if (!dictionary.ContainsKey(controlPoint.faction))
            //        {
            //            dictionary.Add(controlPoint.faction, 1);
            //        }
            //        else
            //        {
            //            dictionary[controlPoint.faction]++;
            //        }
            //        if (controlPoint.controlPointType == ControlPointType.Religion)
            //        {
            //            dictionary[controlPoint.faction] += TemplateManager.global.religionUnityPublicOpinionBonusStrength;
            //        }
            //    }
            //}
            //foreach (TIFactionState key in dictionary.Keys)
            //{
            //    __instance.PropagandaOnPop(key.ideology, (float)dictionary[key] * TemplateManager.global.UnityPublicOpinionBaseStrength);
            //}

            //Below as vanilla
            __instance.AddToCohesion(__instance.unityPriorityCohesionChange);
            __instance.AddToDemocracy(__instance.unityPriorityDemocracyChange);



            return false; //Cancels the call to the original method but runs any other prefixes
        }
    }
}
