using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateAllItemActions
    {

        public static List<IAction> GenerateThingDefActions()
        {
            var allDefs = DefDatabase<ThingDef>.AllDefs.Where(SpawnableThingDefsPredicate);
            List<IAction> allThingActions = allDefs.Select(thingDef => CreateActionFromDef(thingDef)).ToList();
            return allThingActions;
        }

        private static IAction CreateActionFromDef(ThingDef thingDef)
        {
            var category = "Item";

            if (thingDef.IsApparel)
                category = "Apparel";
            else if (thingDef.IsMeleeWeapon)
                category = "Melee Weapons";
            else if (thingDef.IsRangedWeapon)
                category = "Ranged Weapons";
            else if (thingDef.IsWeapon)
                category = "Weapons (other)";
            else if (thingDef.IsDrug)
                category = "Drugs";
            else if (thingDef.defName.ToLower().Contains("meat"))
                category = "Meat";
            else if (thingDef.defName.ToLower().Contains("leather"))
                category = "Leather";
            else if (thingDef.IsIngestible)
                category = "Consumables";
            else if (thingDef.IsMetal)
                category = "Metal";

            return new ItemAction(thingDef, category);
        }

        private static bool SpawnableThingDefsPredicate(ThingDef thingDef)
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
