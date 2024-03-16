using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RimConnection
{
    class DropPodManager
    {
        public static void createDropFromDef(ThingDef thingDef, int amount, string title, string desc, bool showMessage = true, ThingDef stuff = null )
        {
            Thing newthing = ThingMaker.MakeThing(thingDef, stuff ?? null);
            newthing.stackCount = amount;
            newthing.SetForbidden(true);
            if(newthing != null)
            {
                var currentMap = Find.CurrentMap;
                IntVec3? dropVector = DropCellFinder.TryFindSafeLandingSpotCloseToColony(Find.CurrentMap, IntVec2.Two);
                TradeUtility.SpawnDropPod(dropVector ?? DropCellFinder.TradeDropSpot(Find.CurrentMap), currentMap, newthing);
                if (showMessage)
                {
                    string messageString = "RimConnectionPositiveDroppodMailBody".Translate(amount, title, desc);
                    AlertManager.ResourceDropNotification(messageString, dropVector ?? DropCellFinder.TradeDropSpot(Find.CurrentMap));
                }
            }
        }

        public static void createDropOfThings(List<Thing> things, string title, string desc, bool showMessage = true)
        {
            if (things.Count > 0)
            {
                var currentMap = Find.CurrentMap;
                IntVec3? dropVector = DropCellFinder.TryFindSafeLandingSpotCloseToColony(Find.CurrentMap, IntVec2.Two);
                DropPodUtility.DropThingsNear(dropVector ?? DropCellFinder.TradeDropSpot(Find.CurrentMap), currentMap, things, canRoofPunch: false);

                if (showMessage)
                {
                    string messageString = "RimConnectionPositiveDroppodMailBody".Translate("", title, desc);
                    AlertManager.ResourceDropNotification(messageString, dropVector ?? DropCellFinder.TradeDropSpot(Find.CurrentMap));
                }
            }
        }

    }
}
