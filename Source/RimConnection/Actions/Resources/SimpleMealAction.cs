using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class SimpleMealAction : Action
    {
        public SimpleMealAction()
        {
            this.name = "Simple Meal";
            this.description = "Your basic foodstuffs. Won't brighten your day, but you won't starve";
            this.canSpawnMultiple = true;
            this.category = "Resources";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.MealSimple, amount, name, description);
        }
    }
}
