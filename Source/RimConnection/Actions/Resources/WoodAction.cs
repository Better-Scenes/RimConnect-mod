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
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.WoodLog, amount, name, description);
        }
    }
}
