using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class FarmAnimalsWanderInAction : Action
    {
        public FarmAnimalsWanderInAction()
        {
            this.name = "Farm animals join";
            this.description = "Let's just hope the pigs don't take over";
            this.category = "Event";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var animalWorker = new IncidentWorker_FarmAnimalsWanderIn();
            animalWorker.def = IncidentDef.Named("FarmAnimalsWanderIn");
            animalWorker.TryExecute(parms);
        }
    }
}
