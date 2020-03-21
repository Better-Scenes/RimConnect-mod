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
            name = "Spring Clean";
            description = "Heygieia is going to pay a visit";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var allFilth = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Filth).ToList();

            allFilth.ForEach(filthThing =>
            {
                filthThing.Destroy();
            });

            AlertManager.BadEventNotification("The Greek goddess Higieia passed through and took pity on your disgusting base");
        }
    }
}
