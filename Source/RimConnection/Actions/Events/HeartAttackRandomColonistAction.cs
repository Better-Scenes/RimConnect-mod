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
            name = "Sheer Heart Attack";
            description = "Sheeeeeeer Cardiac!";
            category = "Event";
            prefix = "Trigger %amount%";
            shouldShowAmount = true;
        }

        public override void Execute(int amount)
        {
            var heartAttackDef = DefDatabase<HediffDef>.GetNamed("HeartAttack");

            var colonists = Find.ColonistBar.GetColonistsInOrder().Where(colonist => !colonist.Dead);

            var randomColonists = colonists.InRandomOrder().Take(amount);
            foreach (var colonist in randomColonists)
            {
                var heartAttackHeDiff = HediffMaker.MakeHediff(heartAttackDef, colonist);
                colonist.health.AddHediff(heartAttackHeDiff);
            }

            var label = $"Your twitch viewers decided it was better if some of your colonists had heart attacks. I'm so sorry....";
            Find.LetterStack.ReceiveLetter("Twitch Event", label, LetterDefOf.NegativeEvent);
        }
    }
}
