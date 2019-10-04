using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class SolarFlareAction : Action
    {
        public SolarFlareAction()
        {
            this.name = "Solar Flare";
            this.description = "The sun sneezed, not so good for you";
            this.category = "Weather";
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
