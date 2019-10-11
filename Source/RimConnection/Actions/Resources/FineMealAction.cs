using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class FineMealAction : Action
    {
        public FineMealAction()
        {
            this.name = "Meal: Fine";
            this.description = "Fancy restaurant grade";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.MealFine, amount, name, description);
        }
    }
}
