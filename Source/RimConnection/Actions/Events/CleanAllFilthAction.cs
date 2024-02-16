using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class CleanAllFilthAction : Action
    {
        public CleanAllFilthAction()
        {
            Name = "Spring Clean";
            Description = "Hygieia is going to pay a visit";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var allFilth = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Filth).ToList();

            allFilth.ForEach(filthThing =>
            {
                filthThing.Destroy();
            });

            AlertManager.NormalEventNotification("({0}) The Greek goddess Hygieia passed through and took pity on your disgusting base.", boughtBy);
        }
    }
}
