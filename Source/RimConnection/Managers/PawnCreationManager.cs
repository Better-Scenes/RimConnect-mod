using RimWorld;
using Verse;

namespace RimConnection
{
    class PawnCreationManager
    {
        public static void createPawn(int amount, string title, string desc )
        {
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(Find.CurrentMap);
            var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
            var currentMap = Find.CurrentMap;
            GenSpawn.Spawn(newPawn, spawnLocation, currentMap);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, title, desc);
            Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(spawnLocation, currentMap));

        }

    }
}
