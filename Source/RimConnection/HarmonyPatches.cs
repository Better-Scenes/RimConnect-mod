using HarmonyLib;
using RimWorld;
using System.Reflection;
using UnityEngine;
using Verse;

namespace RimConnection
{
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("com.github.harmony.rimworld.mod.rimconnect");

            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Pawn_StoryTracker))]
    [HarmonyPatch("SkinColor", MethodType.Getter)]
    public static class Pawn_StoryTracker_SkinColor_Patch
    {
        private static readonly FieldInfo skinColorField = AccessTools.Field(typeof(Pawn_StoryTracker), nameof(Pawn_StoryTracker.SkinColor));

        [HarmonyPostfix]
        public static void SkinColor_PostFix(Pawn_StoryTracker __instance, ref Color __result)
        {
            Log.Message("Patch Ran");
            __result = new Color();
        }
    }
}