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
            this.name = "Sheer Heart Attack";
            this.description = "Sheeeeeeer Cardiac!";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var heartAttackDef = DefDatabase<HediffDef>.GetNamed("HeartAttack");

            var colonists = Find.ColonistBar.GetColonistsInOrder().Where(colonist => !colonist.Dead);

            var randomColonists = colonists.InRandomOrder().Take(amount);
            foreach (var colonist in randomColonists)
            {
                var heartAttackHeDiff = HediffMaker.MakeHediff(heartAttackDef, colonist);
                colonist.health.AddHediff(heartAttackHeDiff);
            }

            AlertManager.BadEventNotification("Your twitch viewers decided it was better if some of your colonists had heart attacks. I'm so sorry....");
        }
    }
}
