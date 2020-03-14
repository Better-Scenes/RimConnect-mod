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
            name = "Rat Swarm";
            description = "Rats are smarter than you might think, and these ones are pissed";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 rootEntryCell;
            RCellFinder.TryFindRandomPawnEntryCell(out rootEntryCell, currentMap, 0f);
            var rat = DefDatabase<PawnKindDef>.GetNamed("Rat");

            Predicate<IntVec3> validator = (IntVec3 c) => c.InBounds(currentMap) && c.Standable(currentMap);

            var colonistCount = Find.ColonistBar.GetColonistsInOrder().Count;
            var ratCount = colonistCount;

            for (int i = 0; i < ratCount; i++)
            {
                IntVec3 entryCell;
                CellFinder.TryFindRandomCellNear(rootEntryCell, currentMap, 5, validator, out entryCell);
                var newRat = Verse.PawnGenerator.GeneratePawn(rat);
                GenSpawn.Spawn(newRat, entryCell, currentMap);
                newRat.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
            }

            AlertManager.BadEventNotification("A swarm of aggresive rats has appeared. You might need to hide", rootEntryCell);
        }
    }
}
