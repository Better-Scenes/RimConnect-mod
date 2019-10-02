using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class GoldAction : Action
    {
        public GoldAction()
        {
            this.name = "Gold";
            this.description = "Spawn some gliterring gold";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Gold, amount, name, description);
        }
    }
}
