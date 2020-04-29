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
            Name = "Cold Snap";
            Description = "Better rug up!";
            Category = "Weather";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var snapWorker = new IncidentWorker_MakeGameCondition();
            snapWorker.def = IncidentDef.Named("ColdSnap");
            snapWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Cold Snap!");

        }
    }
}
