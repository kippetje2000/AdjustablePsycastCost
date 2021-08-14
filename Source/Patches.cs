using HarmonyLib;
using RimWorld;
using Verse;

namespace Adjustable_Psycast_Cost
{

    [HarmonyPatch(typeof(Pawn_PsychicEntropyTracker), "PsyfocusBand", MethodType.Getter)]
    static class Patch_PsyfocusBand
    {
        [HarmonyPrefix]
        public static bool Prefix(ref int __result)
        {
            __result = Pawn_PsychicEntropyTracker.MaxAbilityLevelPerPsyfocusBand.Count - 1;
            return false;
        }
    }

    [HarmonyPatch(typeof(Ability), "FinalPsyfocusCost")]
    static class Patch_FinalPsyfocusCost
    {
        [HarmonyPrefix]
        public static bool Prefix(Ability __instance, ref LocalTargetInfo target, ref float __result)
        {
            if (__instance.def.AnyCompOverridesPsyfocusCost)
            {
                foreach (AbilityComp abilityComp in __instance.comps)
                {
                    if (abilityComp.props.OverridesPsyfocusCost)
                    {
                        __result = abilityComp.PsyfocusCostForTarget(target);
                        return false;
                    }
                }
            }
            if (Adjustable_Psycast_Cost.settings.psycastCostAll.GetValueSafe(__instance.def.defName) / 100 != 0)
            {
                __result = Adjustable_Psycast_Cost.settings.psycastCostAll.GetValueSafe(__instance.def.defName) / 100;
                return false;
            }
            __result = __instance.def.PsyfocusCost;
            return false;
        }

    }

    [HarmonyPatch(typeof(Command_Psycast), "get_TopRightLabel")]
    static class Patch_TopRightLabel
    {
        [HarmonyPrefix]
        public static bool Prefix(ref string __result, Ability ___ability)
        {
            AbilityDef def = ___ability.def;
            string text = "";
            if (def.EntropyGain > 1E-45f)
            {
                text += "NeuralHeatLetter".Translate() + ": " + def.EntropyGain.ToString() + "\n";
            }
            if (def.PsyfocusCost > 1E-45f)
            {
                string t;
                if (def.AnyCompOverridesPsyfocusCost)
                {
                    if (def.PsyfocusCostRange.Span > 1E-45f)
                    {
                        t = def.PsyfocusCostRange.min * 100f + "-" + def.PsyfocusCostPercentMax;
                    }
                    else
                    {
                        t = def.PsyfocusCostPercentMax;
                    }
                }
                else
                {
                    if (Adjustable_Psycast_Cost.settings.psycastCostAll.GetValueSafe(def.defName) != 0)
                    {
                        t = Adjustable_Psycast_Cost.settings.psycastCostAll.GetValueSafe(def.defName).ToString() + "%";
                    }
                    else
                    {
                        t = def.PsyfocusCostPercent;
                    }
                }

                text += "PsyfocusLetter".Translate() + ": " + t;
            }

            __result = text.TrimEndNewlines();
            return false;
        }
    }
}