using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class TornadoAction: Action, IAction
    {
        public TornadoAction()
        {
            Name = "Tornado";
            Description = "A destructive force of wind";
            Category = "Orbital";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            Map currentMap = Find.CurrentMap;

            CellRect cellRect = CellRect.WholeMap(currentMap).ContractedBy(30);
            if (cellRect.IsEmpty)
            {
                cellRect = CellRect.WholeMap(currentMap);
            }

            IntVec3 location;
            if(CellFinder.TryFindRandomCellInsideWith(cellRect, (IntVec3 x) => true, out location))
            {
                GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("Tornado"), location, currentMap);
                AlertManager.BadEventNotification("A tornado has been summoned by {0}. Let's hope it doesn't rip through your base.", location, boughtBy);
            }

        }
    }
}