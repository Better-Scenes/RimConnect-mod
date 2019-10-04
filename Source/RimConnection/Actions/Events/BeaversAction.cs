using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class BeaversAction : Action
    {
        public BeaversAction()
        {
            this.name = "Beavers";
            this.description = "Beavers might be cute, but they're about to mess you up";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_Alphabeavers().TryExecute(parms);
        }
    }
}
