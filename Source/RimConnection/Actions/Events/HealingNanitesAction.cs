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
            name = "Healing Nanites";
            description = "A wave of nanites fly through and heal superficial wounds";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = -1;
        }

        private static List<HediffDef> hediffsDefsToHeal = new List<HediffDef>(new HediffDef[] {
                HediffDefOf.Bite,
                HediffDefOf.Burn,
                HediffDefOf.Cut,
                HediffDefOf.Stab,
                HediffDefOf.Scratch,
                HediffDefOf.Hangover,
                HediffDefOf.Bruise,
                DefDatabase<HediffDef>.GetNamed("Crack"),
                DefDatabase<HediffDef>.GetNamed("Crush")
        });

        public override void Execute(int amount)
        {
            var colonists = Find.ColonistBar.GetColonistsInOrder();

            colonists.ForEach(colonist =>
            {
                var healableHediffs = colonist.health.hediffSet.hediffs.Where(hediff => hediffsDefsToHeal.Contains(hediff.def)).ToList();
                healableHediffs.ForEach(hediff => colonist.health.RemoveHediff(hediff));
            });

            AlertManager.NormalEventNotification("A wave of nanites has flown through you colony and healed everyone's superficial wounds!");
        }
    }
}
