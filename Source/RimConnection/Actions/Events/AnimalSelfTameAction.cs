using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AnimalSelfTameAction : Action
    {
        public AnimalSelfTameAction()
        {
            this.name = "Animal Self Tame";
            this.description = "You don't get to choose, hope it was a good one";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_SelfTame().TryExecute(parms);
            AlertManager.NormalEventNotification("Your viewers have tamed some animals!");
        }
    }
}
