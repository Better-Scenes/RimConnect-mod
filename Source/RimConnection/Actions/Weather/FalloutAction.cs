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
            Name = "Toxic Fallout";
            Description = "The wasteland has arrived";
            Category = "Weather";
            Prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var worker = IncidentDefOf.ToxicFallout;
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            worker.Worker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Fallout!");

        }
    }
}
