using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection;

namespace RimConnection
{
    [HarmonyPatch(typeof(Pawn_StoryTracker))]
    [HarmonyPatch("SkinColor", MethodType.Getter)]
    static class HarmonyPatches2
    {
        static HarmonyPatches2()
        {
            Harmony harmony = new Harmony("com.github.harmony.rimworld.mod.rimconnect");
            harmony.PatchAll();
        }

        private static readonly FieldInfo skinColorField = AccessTools.Field(typeof(Pawn_StoryTracker), nameof(Pawn_StoryTracker.SkinColor));

        [HarmonyPostfix]
        static Color SkinColorBlue(Pawn_StoryTracker __instance, ref Color __result)
        {
            Log.Message("Patch2 running");
            if (Current.Game != null)
            {
                BlueSkinTracker blueSkinComponent = Current.Game.GetComponent<BlueSkinTracker>();
                Pawn pawn = (Pawn)typeof(Pawn_StoryTracker).GetField("pawn").GetValue(__instance);

                if (blueSkinComponent.bluePawns.Contains(pawn))
                {
                    __result = new Color(64, 70, 255);
                }
            }

            return __result;
        }
    }
}
