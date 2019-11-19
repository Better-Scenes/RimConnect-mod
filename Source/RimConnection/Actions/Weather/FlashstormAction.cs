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
            name = "Flashstorm";
            description = "Fast as lightning, a little bit frightening";
            category = "Weather";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var flashWorker = new IncidentWorker_Flashstorm();
            flashWorker.def = IncidentDef.Named("Flashstorm");

            flashWorker.TryExecute(parms);
        }
    }
}
