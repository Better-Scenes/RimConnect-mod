using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class EclipseAction : Action
    {
        public EclipseAction()
        {
            name = "Eclipse";
            description = "Time to go where the sun don't shine";
            category = "Weather";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var worker = IncidentDefOf.Eclipse;
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            worker.Worker.TryExecute(parms);
        }
    }
}
