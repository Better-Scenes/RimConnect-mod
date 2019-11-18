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

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var snapWorker = new IncidentWorker_MakeGameCondition();
            snapWorker.def = IncidentDef.Named("ColdSnap");

            snapWorker.TryExecute(parms);
        }
    }
}
