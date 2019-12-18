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
            name = "Raining Men";
            description = "These men are uh.... A little old and unrefigerated";
            category = "Event";
            shouldShowAmount = false;
            prefix = "Trigger";
            costSilverStore = -1;
            costBitStore = 300;
        }

        public override void Execute(int amount)
        {
            var amountOfDrops = 15;
            var currentMap = Find.CurrentMap;

            var pawnGenerationRequest = new PawnGenerationRequest(PawnKindDefOf.Colonist, allowDead: true);
            for (int i = 0; i < amountOfDrops; i ++ )
            {
                var newPawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);

                IntVec3 dropVector = DropCellFinder.RandomDropSpot(currentMap);
                TradeUtility.SpawnDropPod(dropVector, currentMap, newPawn);

                HealthUtility.DamageUntilDead(newPawn);
                newPawn.equipment.DestroyAllEquipment();
                newPawn.apparel.DestroyAll();
            }

            AlertManager.NormalEventNotification("It's Raining men, hallelujah!");
        }
    }
}
