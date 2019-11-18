using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class FalloutAction : Action
    {
        public FalloutAction()
        {
            this.name = "Toxic Fallout";
            this.description = "The wasteland has arrived";
            this.category = "Weather";
        }

        public override void Execute(int amount)
        {
            var worker = IncidentDefOf.ToxicFallout;
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            worker.Worker.TryExecute(parms);
        }
    }
}
