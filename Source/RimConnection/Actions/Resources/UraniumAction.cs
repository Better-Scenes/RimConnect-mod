using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class UraniumAction : Action
    {
        public UraniumAction()
        {
            this.name = "Uranium";
            this.description = "Not fit for human consumption";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Uranium, amount, name, description);
        }
    }
}
