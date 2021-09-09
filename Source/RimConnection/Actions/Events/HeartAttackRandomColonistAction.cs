using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class HeartAttackRandomColonistAction : Action
    {
        public HeartAttackRandomColonistAction()
        {
            Name = "Sheer Heart Attack";
            Description = "Sheeeeeeer Cardiac!";
            Category = "Event";
            Prefix = "Trigger %amount%";
            ShouldShowAmount = true;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var heartAttackDef = DefDatabase<HediffDef>.GetNamed("HeartAttack");

            var colonists = Find.ColonistBar.GetColonistsInOrder().Where(colonist => !colonist.Dead);

            var randomColonists = colonists.InRandomOrder().Take(amount);
            foreach (var colonist in randomColonists)
            {
                var heartAttackHeDiff = HediffMaker.MakeHediff(heartAttackDef, colonist);
                colonist.health.AddHediff(heartAttackHeDiff);
            }

            AlertManager.BadEventNotification("{0} decided it was better if some of your colonists had heart attacks. I'm so sorry...", boughtBy);
        }
    }
}
