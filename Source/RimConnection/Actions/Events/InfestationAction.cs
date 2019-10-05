using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class InfestationAction : Action
    {
        public InfestationAction()
        {
            this.name = "Infestation";
            this.description = "You just had to go deeper didn't you";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var infestationWorker = new IncidentWorker_Infestation();
            infestationWorker.def = IncidentDef.Named("Infestation");

            infestationWorker.TryExecute(parms);


        }
    }
}
