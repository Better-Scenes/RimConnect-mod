using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateInspirationActions
    {

        public static List<IAction> GenerateInspirationDefActions()
        {
            IEnumerable<InspirationDef> allDefs = DefDatabase<InspirationDef>.AllDefs;
            List<IAction> allInspirationActions = allDefs.Select(inspirationDef => CreateActionFromDef(inspirationDef)).ToList();
            return allInspirationActions;
        }

        private static IAction CreateActionFromDef(InspirationDef inspirationDef)
        {
            return new InspirationAction(inspirationDef);
        }
    }
}
