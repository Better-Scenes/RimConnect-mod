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
            Name = "Flashstorm";
            Description = "Fast as lightning, a little bit frightening";
            Category = "Weather";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var flashWorker = new IncidentWorker_Flashstorm();
            flashWorker.def = IncidentDef.Named("Flashstorm");

            flashWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Flashstorm");

        }
    }
}
