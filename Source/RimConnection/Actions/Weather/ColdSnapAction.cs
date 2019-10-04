using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class ColdSnapAction : Action
    {
        public ColdSnapAction()
        {
            this.name = "Cold Snap";
            this.description = "Better rug up!";
            this.category = "Weather";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_ColdSnap().TryExecute(parms);
        }
    }
}
