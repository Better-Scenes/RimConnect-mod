using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class GlitterworldMedicineAction : Action
    {
        public GlitterworldMedicineAction()
        {
            this.name = "Glitterworld Medicine";
            this.description = "All that glitters is not gold";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.MedicineUltratech, amount, name, description);
        }
    }
}
