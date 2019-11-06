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
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.MedicineHerbal, amount, name, description);
        }
    }
}
