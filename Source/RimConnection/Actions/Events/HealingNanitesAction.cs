using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class HealingNanitesAction : Action
    {
        public HealingNanitesAction()
        {
            Name = "Healing Nanites";
            Description = "A wave of nanites fly through and heal superficial wounds";
            Category = "Event";
            Prefix = "Trigger";
        }

        private static List<HediffDef> hediffsDefsToHeal = new List<HediffDef>(new HediffDef[] {
                HediffDefOf.Bite,
                DefDatabase<HediffDef>.GetNamed("Burn"),
                HediffDefOf.Cut,
                DefDatabase<HediffDef>.GetNamed("Stab"),
                DefDatabase<HediffDef>.GetNamed("Scratch"),
                HediffDefOf.Hangover,
                DefDatabase<HediffDef>.GetNamed("Bruise"),
                DefDatabase<HediffDef>.GetNamed("Crack")
        });

        public override void Execute(int amount, string boughtBy)
        {
            var colonists = Find.ColonistBar.GetColonistsInOrder();

            colonists.ForEach(colonist =>
            {
                var healableHediffs = colonist.health.hediffSet.hediffs.Where(hediff => hediffsDefsToHeal.Contains(hediff.def)).ToList();
                healableHediffs.ForEach(hediff => colonist.health.RemoveHediff(hediff));
            });

            AlertManager.NormalEventNotification("({0}) A wave of nanites has flown through your colony and healed everyone's superficial wounds!", boughtBy);
        }
    }
}
