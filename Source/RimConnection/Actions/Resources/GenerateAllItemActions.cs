using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateAllItmeActions
    {

        public static List<IAction> GenerateThingDefActions()
        {
            var allDefs = DefDatabase<ThingDef>.AllDefs.Where(spawnableThingDefsPredicate);
            List<IAction> allThingActions = allDefs.Select(thingDef => (IAction) new ItemAction(thingDef)).ToList();
            return allThingActions;
        }

        private static bool spawnableThingDefsPredicate(ThingDef thingDef)
        {
            return (
                (thingDef.tradeability.TraderCanSell() || ThingSetMakerUtility.CanGenerate(thingDef)) &&
                thingDef.building == null &&
                (thingDef.FirstThingCategory != null || thingDef.race != null) &&
                thingDef.BaseMarketValue > 0
                );
        }
    }
}
