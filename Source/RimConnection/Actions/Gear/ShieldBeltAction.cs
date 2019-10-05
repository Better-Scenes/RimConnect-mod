using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class ShieldBeltAction : Action
    {
        public ShieldBeltAction()
        {
            this.name = "Shield Belt";
            this.description = "The only way in or out is a tiny hole at the top";
            this.canSpawnMultiple = true;
            this.category = "Gear";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Apparel_ShieldBelt, amount, name, description);
        }
    }
}
