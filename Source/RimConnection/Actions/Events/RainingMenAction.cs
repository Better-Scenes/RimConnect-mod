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
            Name = "Raining Men";
            Description = "These men are uh.... A little old and unrefigerated";
            Category = "Event";
            ShouldShowAmount = false;
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var amountOfDrops = 20;
            var currentMap = Find.CurrentMap;

            var pawnGenerationRequest = new PawnGenerationRequest(PawnKindDefOf.Colonist, allowDead: true);
            for (int i = 0; i < amountOfDrops; i ++ )
            {
                var newPawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);

                IntVec3 dropVector = DropCellFinder.TradeDropSpot(currentMap);
                TradeUtility.SpawnDropPod(dropVector, currentMap, newPawn);

                HealthUtility.DamageUntilDead(newPawn);
                newPawn.equipment.DestroyAllEquipment();
                newPawn.apparel.DestroyAll();
            }

            AlertManager.NormalEventNotification("({0}) It's Raining men, hallelujah!", boughtBy);
        }
    }
}
