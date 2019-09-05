using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class GeothermalAction : Action
    {
        public GeothermalAction()
        {
            this.name = "Geothermal Generator";
            this.description = "Steamy hot steam";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.GeothermalGenerator, amount);
        }
    }
}
