using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class ClearAddictionsAction : Action
    {
        public ClearAddictionsAction()
        {
            Name = "Clear Addictions";
            Description = "All the colonists suddenly feel free";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var colonists = Find.ColonistBar.GetColonistsInOrder();

            colonists.ForEach(colonist =>
            {
                List<Hediff_Addiction> hediffs = new List<Hediff_Addiction>();
                colonist.health.hediffSet.GetHediffs(ref hediffs);
                hediffs.ForEach(hediff => { colonist.health.RemoveHediff(hediff); });
            });

            AlertManager.NormalEventNotification("{0} wanted to help you out this time and removed all addictions", boughtBy);
        }
    }
}
