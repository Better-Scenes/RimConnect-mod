using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class SteelAction : Action
    {
        public SteelAction()
        {
            this.name = "Steel";
            this.description = "Heard a great joke about metal, thought I'd steel it";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Steel, amount, name, description);
        }
    }
}
