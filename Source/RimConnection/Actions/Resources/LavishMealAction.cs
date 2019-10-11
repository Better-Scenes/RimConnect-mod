using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class LavishMealAction : Action
    {
        public LavishMealAction()
        {
            this.name = "Lavish Meal";
            this.description = "Fit for a king!";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            var lavishMealDef = ThingDef.Named("MealLavish");
            DropPodManager.createDrop(lavishMealDef, amount, name, description);
        }
    }
}
