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
            Name = "Heat Wave";
            Description = "Is it just me or is it hot in here?";
            Category = "Weather";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var heatwaveWorker = new IncidentWorker_MakeGameCondition();
            heatwaveWorker.def = IncidentDef.Named("HeatWave");

            heatwaveWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Heat Wave!");

        }
    }
}
