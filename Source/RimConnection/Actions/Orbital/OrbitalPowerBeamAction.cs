using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class OrbitalPowerBeamAction: Action, IAction
    {
        public OrbitalPowerBeamAction()
        {
            Name = "Orbital Power Beam";
            Description = "A destructive beam of heat";
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
                PowerBeam powerBeam = (PowerBeam) GenSpawn.Spawn(ThingDefOf.PowerBeam, location, currentMap);
                powerBeam.duration = 600;
                powerBeam.StartStrike();
                AlertManager.BadEventNotification("{0} requested a bombardment from space!", location, boughtBy);
            }

        }
    }
}