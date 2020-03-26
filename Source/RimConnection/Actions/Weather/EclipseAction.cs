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
            Name = "Eclipse";
            Description = "Time to go where the sun don't shine";
            Category = "Weather";
            Prefix = "Trigger";
            CostSilverStore = 600;
        }

        public override void Execute(int amount)
        {
            var worker = IncidentDefOf.Eclipse;
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            worker.Worker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent an Eclipse!");

        }
    }
}
