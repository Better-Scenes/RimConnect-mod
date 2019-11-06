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
            for(int i = 0; i < amountOfDrops; i ++ )
            {
                var currentMap = Find.CurrentMap;
                IntVec3 dropVector = DropCellFinder.TradeDropSpot(Find.CurrentMap);
                //TradeUtility.SpawnDropPod(dropVector, currentMap, newthing);

                Find.LetterStack.ReceiveLetter("Twitch Drop", "Some bodies are coming your way", LetterDefOf.NegativeEvent, new TargetInfo(dropVector, currentMap));
            }

            Find.LetterStack.ReceiveLetter("Twitch Event", "It's Raining men, hallelujah!", LetterDefOf.NeutralEvent);
        }
    }
}
