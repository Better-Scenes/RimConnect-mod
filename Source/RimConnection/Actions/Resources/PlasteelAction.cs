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
            this.description = "Not quite plastic, not quite steel, but somehow better than the sum of its parts";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Plasteel, amount, name, description);
        }
    }
}
