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

            var randomColonists = colonists.InRandomOrder().Take(amount);
            var colonistNames = new List<string>();
            foreach (var colonist in randomColonists)
            {
                colonistNames.Add(colonist.Name.ToStringShort);
                colonist.Kill(new DamageInfo(DamageDefOf.Crush, 999999));
            }

            string colonistsAsJoinedString;
            if (colonistNames.Count == 1)
            {
                colonistsAsJoinedString = colonistNames[0];
            }
            else
            {
                var allColonistsExceptOneNames = colonistNames.Take(colonistNames.Count - 1);
                var lastColonistName = colonistNames[colonistNames.Count - 1];

                colonistsAsJoinedString = String.Join(", ", allColonistsExceptOneNames.ToArray());
                colonistsAsJoinedString += $", and {lastColonistName}";
            }

            var label = $"Your twitch viewers decided it was better if {colonistsAsJoinedString} weren't around anymore";
            Find.LetterStack.ReceiveLetter("Twitch Event", label, LetterDefOf.NegativeEvent);
        }
    }
}
