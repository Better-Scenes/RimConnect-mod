using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateHeDiffActions
    {

        public static List<IAction> GenerateHeDiffDefActions()
        {
            IEnumerable<HediffGiver> heDiffGivers = PawnKindDefOf.Colonist.RaceProps.hediffGiverSets.SelectMany((HediffGiverSetDef set) => set.hediffGivers);
            List<IAction> allConditionActions = heDiffGivers.Select(hediffGiver => CreateActionFromDef(hediffGiver)).ToList();

            return allConditionActions;
        }

        private static IAction CreateActionFromDef(HediffGiver hediffGiver)
        {
            return new HeDiffAction(hediffGiver.hediff);
        }
    }
}
