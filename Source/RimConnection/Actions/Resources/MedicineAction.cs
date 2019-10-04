using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class MedicineAction : Action
    {
        public MedicineAction()
        {
            this.name = "Medicine";
            this.description = "Patch yourself up!";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.MedicineIndustrial, amount, name, description);
        }
    }
}
