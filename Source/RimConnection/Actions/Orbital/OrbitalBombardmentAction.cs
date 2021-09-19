using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class OrbitalBombardmentAction: Action, IAction
    {
        public OrbitalBombardmentAction()
        {
            Name = "Orbital Bombardment";
            Description = "A destructive bombardment";
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
                Bombardment bombardment = (Bombardment) GenSpawn.Spawn(ThingDefOf.Bombardment, location, currentMap);
                bombardment.StartStrike();
                AlertManager.BadEventNotification("{0} send a bombardment from space!", location, boughtBy);
            }

        }
    }
}