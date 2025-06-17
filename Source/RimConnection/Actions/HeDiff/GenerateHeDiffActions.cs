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
            var sets = PawnKindDefOf.Colonist
                       ?.RaceProps
                       ?.hediffGiverSets;
            if (sets == null)
                return new List<IAction>();

            return sets
                .SelectMany(set => set?.hediffGivers ?? Enumerable.Empty<HediffGiver>())
                .Where(g => g?.hediff != null)
                .Select(g => (IAction)new HeDiffAction(g.hediff))
                .ToList();
        }
    }
}
