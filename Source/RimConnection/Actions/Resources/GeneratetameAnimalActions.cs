using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateTameAnimalActions
    {

        public static List<IAction> GeneratetameAnimalDefActions()
        {
            IEnumerable<ThingDef> allDefs = DefDatabase<ThingDef>.AllDefs.Where(SpawnableThingDefsPredicate);
            List<IAction> allThingActions = allDefs.Select(thingDef => CreateActionFromDef(thingDef)).ToList();
            return allThingActions;
        }

        private static IAction CreateActionFromDef(ThingDef thingDef)
        {
              return new TameAnimalAction(thingDef, "Tame Animals");
        }

        private static bool SpawnableThingDefsPredicate(ThingDef thingDef)
        {
            return (
                (thingDef.tradeability.TraderCanSell() || ThingSetMakerUtility.CanGenerate(thingDef)) &&
                (thingDef.building == null || thingDef.Minifiable ) &&
                thingDef.BaseMarketValue > 0 &&
                thingDef.race != null
                );
        }
    }
}
