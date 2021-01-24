using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class MegaTornadoAction : Action, IAction
    {
        private int numberToSpawn = 5;

        public MegaTornadoAction()
        {
            Name = "Mega Tornado";
            Description = "This one is pretty terrifying";
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
                for(int i = 0; i < numberToSpawn; i++)
                {
                    GenSpawn.Spawn(ThingDefOf.Tornado, location, currentMap);
                }
                AlertManager.BadEventNotification("A Mega Tornado has been formed. I'd just suggest as far away as possible", location);
            }

        }
    }
}