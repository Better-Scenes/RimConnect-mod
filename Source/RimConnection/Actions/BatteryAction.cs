using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class BatteryAction : Action
    {
        public BatteryAction()
        {
            this.name = "Battery";
            this.description = "Make sure those electrons don't escape and make them do your bidding";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Battery, amount);
        }
    }
}
