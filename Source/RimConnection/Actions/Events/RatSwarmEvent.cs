using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RatSwarmEvent : Action
    {
        public RatSwarmEvent()
        {
            this.name = "Rat Swarm";
            this.description = "Rats are smarter than you might think, and these ones are pissed";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            var entryCell = CellFinder.RandomEdgeCell(currentMap);
            var rat = DefDatabase<PawnKindDef>.GetNamed("Rat");

            var colonistCount = Find.ColonistBar.GetColonistsInOrder().Count;
            var ratCount = 2 * colonistCount;

            for (int i = 0; i < ratCount; i++)
            {
                var newRat = Verse.PawnGenerator.GeneratePawn(rat);
                GenSpawn.Spawn(newRat, entryCell, currentMap);
                newRat.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
            }
           
            Find.LetterStack.ReceiveLetter("Twitch Event", "A swarm of aggresive rats has appeared. You might need to hide", LetterDefOf.NegativeEvent, new LookTargets(entryCell, currentMap));
        }
    }
}
