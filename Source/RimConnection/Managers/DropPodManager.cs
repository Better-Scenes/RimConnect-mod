using Multiplayer.API;
using RimWorld;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Verse;
using System.Linq;

namespace RimConnection
{
    class DropPodManager : GameComponent
    {

        public DropPodManager(Game game)
        {
        }

        static ConcurrentQueue<Thing> dropQueue = new ConcurrentQueue<Thing>();
        private static IntVec3 dropVector;
        private static Map currentMap;

        public override void GameComponentTick()
        {
            if (dropQueue.Count > 0)
            {
                List<Thing> dropList = dropQueue.ToList();
                dropSpawn(currentMap, dropVector, dropList);
            }
        }

        [SyncMethod]
        private static void dropSpawn(Map currentMap, IntVec3 dropVector, List<Thing> newthings)
        {
            DropPodUtility.DropThingsNear(dropVector, currentMap, newthings);

        }

        public static void createDropFromDef(ThingDef thingDef, int amount, string title, string desc, bool showMessage = true, ThingDef stuff = null )
        {
            Thing newthing = ThingMaker.MakeThing(thingDef, stuff ?? null);
            newthing.stackCount = amount;
            newthing.SetForbidden(true);
            if(newthing != null)
            {
                currentMap = Find.CurrentMap;
                Rand.PushState();
                dropVector = DropCellFinder.TradeDropSpot(currentMap);
                Rand.PopState();

                dropQueue.Enqueue(newthing);

                if (showMessage)
                {
                    string messageString = "RimConnectionPositiveDroppodMailBody".Translate(amount, title, desc);
                    AlertManager.ResourceDropNotification(messageString, dropVector);
                }
            }
        }

        public static void createDropOfThings(List<Thing> things, string title, string desc, bool showMessage = true)
        {
            if (things.Count > 0)
            {
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector;

                if (MP.IsInMultiplayer)
                {
                    dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                    DropPodUtility.DropThingsNear(dropVector, currentMap, things);
                }
                else
                {
                    dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                    DropPodUtility.DropThingsNear(dropVector, currentMap, things);
                }

                if (showMessage)
                {
                    string messageString = "RimConnectionPositiveDroppodMailBody".Translate("", title, desc);
                    AlertManager.ResourceDropNotification(messageString, dropVector);
                }
            }
        }

    }
}
