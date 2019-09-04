using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class PlasteelAction : Action
    {
        public PlasteelAction()
        {
            this.name = "Plasteel";
            this.description = "Spawn some dense Plasteel";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Plasteel, amount);
        }
    }
}
