using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class HerbalMedicineAction : Action
    {
        public HerbalMedicineAction()
        {
            this.name = "Herbal Medicine";
            this.description = "Sticks and stones...";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.MedicineHerbal, amount, name, description);
        }
    }
}
