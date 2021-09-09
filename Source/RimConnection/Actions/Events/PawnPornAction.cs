using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class PawnPornAction : Action
    {
        public PawnPornAction()
        {
            Name = "Pawn Porn";
            Description = "More muff than a herd of Muffalo";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            IEnumerable<Pawn> colonists = Find.ColonistBar.GetColonistsInOrder();

            foreach (Pawn colonist in colonists)
            {
                colonist.apparel.DropAll(colonist.Position, false);
            }

            AlertManager.NormalEventNotification("({0}) Everyone decided that clothes weren't a thing they needed to wear anymore.", boughtBy);
        }
    }
}
