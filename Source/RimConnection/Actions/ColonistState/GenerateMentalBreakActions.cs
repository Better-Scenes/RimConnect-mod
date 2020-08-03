using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateMentalBreakActions
    {

        public static List<IAction> GenerateMentalBreakDefActions()
        {
            IEnumerable<MentalBreakDef> allDefs = DefDatabase<MentalBreakDef>.AllDefs;
            List<IAction> allMentalBreakActions = allDefs.Select(mentalBreakDef => CreateActionFromDef(mentalBreakDef)).ToList();
            return allMentalBreakActions;
        }

        private static IAction CreateActionFromDef(MentalBreakDef mentalBreakDef)
        {
            return new MentalBreakAction(mentalBreakDef);
        }
    }
}
