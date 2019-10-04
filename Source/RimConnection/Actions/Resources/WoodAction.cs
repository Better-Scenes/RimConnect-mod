using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class WoodAction : Action
    {
        public WoodAction()
        {
            this.name = "Wood";
            this.description = "Better than bad its good";
            this.canSpawnMultiple = true;
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.WoodLog, amount, name, description);
        }
    }
}
