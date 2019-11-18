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
            this.name = "Alpha Beavers";
            this.description = "Beavers might be cute, but they'll eat all your stuff";
            this.category = "Event";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var beaverWorker = new IncidentWorker_Alphabeavers();
            beaverWorker.def = IncidentDef.Named("Alphabeavers");
            beaverWorker.TryExecute(parms);
        }
    }
}
