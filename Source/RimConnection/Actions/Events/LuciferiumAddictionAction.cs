using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class LuciferiumAddictionAction : Action
    {
        public LuciferiumAddictionAction()
        {
            name = "Luciferium Addction";
            description = "Luciferium addiction descpriotnadsgfhlkj";
            category = "Event";
        }

        public override void Execute(int amount)
        {
            var amountMultiplier = 10;
            var colonists = Find.ColonistBar.GetColonistsInOrder();

            var luciferiumBenefitHeDiffDef = DefDatabase<HediffDef>.GetNamed("LuciferiumHigh");
            var luciferiumAdditictionHeDiffDef = DefDatabase<HediffDef>.GetNamed("LuciferiumAddiction");

            colonists.ForEach(colonist =>
            {
                colonist.health.AddHediff(luciferiumBenefitHeDiffDef);
                colonist.health.AddHediff(luciferiumAdditictionHeDiffDef);
            });

            var amountToDrop = colonists.Count() * amountMultiplier;
            DropPodManager.createDropFromDef(ThingDefOf.Luciferium, amountToDrop, "", "", false);

            AlertManager.NormalEventNotification("Your viewers decided that everyone should have a life threatening addiction, along with some supplies");
        }
    }
}
