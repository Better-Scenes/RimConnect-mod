using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RainingMenAction : Action
    {
        public RainingMenAction()
        {
            this.name = "Raining Men";
            this.description = "These men are uh.... A little old and unrefigerated";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var amountOfDrops = 15;

            var pawnToDrop = PawnCreationManager.generateDefaultColonists(amountOfDrops, Faction.OfAncients);

            for (int i = 0; i < amountOfDrops; i ++ )
            {
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector = DropCellFinder.TradeDropSpot(currentMap);
                TradeUtility.SpawnDropPod(dropVector, currentMap, pawnToDrop[i]);
                pawnToDrop[i].Kill();
            }

            Find.LetterStack.ReceiveLetter("Twitch Event", "It's Raining men, hallelujah!", LetterDefOf.NeutralEvent);
        }
    }
}
