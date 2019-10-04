using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class SilverAction : Action
    {
        public SilverAction()
        {
            this.name = "Silver";
            this.description = "Cash money!";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Silver, amount, name, description);
        }
    }
}
