using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AnestheticRandomColonistAction : Action
    {
        public AnestheticRandomColonistAction()
        {
            Name = "Anesthetic";
            Description = "Time to sleep now";
            Category = "Event";
            Prefix = "Trigger %amount%";
            ShouldShowAmount = true;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var AnestheticDef = DefDatabase<HediffDef>.GetNamed("Anesthetic");

            var colonists = Find.ColonistBar.GetColonistsInOrder().Where(colonist => !colonist.Dead);

            var randomColonists = colonists.InRandomOrder().Take(amount);
            foreach (var colonist in randomColonists)
            {
                var AnestheticHeDiff = HediffMaker.MakeHediff(AnestheticDef, colonist);
                colonist.health.AddHediff(AnestheticHeDiff);
            }

            AlertManager.BadEventNotification("Your twitch viewers decided some people should have a rest");
        }
    }
}
