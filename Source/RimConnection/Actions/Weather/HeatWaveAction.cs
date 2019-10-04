using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class HeatWaveAction : Action
    {
        public HeatWaveAction()
        {
            this.name = "Heat Wave";
            this.description = "Is it just me or is it hot in here?";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_HeatWave().TryExecute(parms);
        }
    }
}
