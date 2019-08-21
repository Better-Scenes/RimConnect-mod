using RimWorld;
using Verse;

namespace RimConnection
{
    class DropPodManager
    {
        public static void createDrop()
        {
            Thing newthing = ThingMaker.MakeThing(ThingDefOf.Gold);
            newthing.stackCount = 100;
            if(newthing != null)
            {
                string labelString = "RimConnectionDroppodMailLabel".Translate() ;
                string messageString = "RimConnectionPositiveDroppodMailBody".Translate("InfinitySamurai");
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                TradeUtility.SpawnDropPod(dropVector, currentMap, newthing);
                Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(dropVector, currentMap));
            }
        }

    }
}
