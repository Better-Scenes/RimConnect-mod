using RimWorld;
using Verse;

namespace RimConnection
{
    class PawnCreationManager
    {
        public static void createPawn(int amount )
        {
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(Find.CurrentMap);
            var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
            GenSpawn.Spawn(newPawn, spawnLocation, Find.CurrentMap);
        }

    }
}
