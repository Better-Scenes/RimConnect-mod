using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class KillRandomColonistAction : Action
    {
        public KillRandomColonistAction()
        {
            this.name = "Kill random colonist";
            this.description = "You must be awfully cruel to select something like this";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var colonists = Find.ColonistBar.GetColonistsInOrder();

            var randomColonists = colonists.TakeRandom(amount);

            foreach (var colonist in randomColonists)
            {
                colonist.Kill(new DamageInfo(DamageDefOf.Crush, 999999));
            }
        }
    }
}
