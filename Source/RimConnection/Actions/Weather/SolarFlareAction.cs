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
            Name = "Solar Flare";
            Description = "The sun sneezed, not so good for you";
            Category = "Weather";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var solarWorker = new IncidentWorker_MakeGameCondition();
            solarWorker.def = IncidentDef.Named("SolarFlare");

            solarWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Solar Flare");

        }
    }
}
