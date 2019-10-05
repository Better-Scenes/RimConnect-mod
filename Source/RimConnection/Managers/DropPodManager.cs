using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RimConnection
{
    class DropPodManager
    {
        public static void createDrop(ThingDef thingDef, int amount, string title, string desc)
        {
            Thing newthing = ThingMaker.MakeThing(thingDef);
            newthing.stackCount = amount;
            if(newthing != null)
            {
                string labelString = "RimConnectionDroppodMailLabel".Translate() ;
                string messageString = "RimConnectionPositiveDroppodMailBody".Translate(amount, title, desc);
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                TradeUtility.SpawnDropPod(dropVector, currentMap, newthing);
                Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(dropVector, currentMap));
            }
        }

        public static void createDropOfThings(List<Thing> things, string title, string desc)
        {
            if (things.Count > 0)
            {
                string labelString = "RimConnectionDroppodMailLabel".Translate();
                string messageString = "RimConnectionPositiveDroppodMailBody".Translate("", title, desc);
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                DropPodUtility.DropThingsNear(dropVector, currentMap, things);
                Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(dropVector, currentMap));
            }
        }

    }
}
