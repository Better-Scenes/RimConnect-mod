using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class NutrientPasteAction : Action
    {
        public NutrientPasteAction()
        {
            this.name = "Meal: Nutrient Paste";
            this.description = "Not the worst thing to eat, but pretty close";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.MealNutrientPaste, amount, name, description);
        }
    }
}
