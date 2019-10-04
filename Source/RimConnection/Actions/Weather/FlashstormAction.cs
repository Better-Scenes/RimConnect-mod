using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class FlashstormAction : Action
    {
        public FlashstormAction()
        {
            this.name = "Flashstorm";
            this.description = "Fast as lightning, a little bit frightening";
            this.category = "Weather";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_Flashstorm().TryExecute(parms);
        }
    }
}
