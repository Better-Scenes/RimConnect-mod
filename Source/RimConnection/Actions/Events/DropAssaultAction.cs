using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class DropAssaultAction : Action
    {
        public DropAssaultAction()
        {
            this.name = "Drop Assault";
            this.description = "They were inside the house the whole time!";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.CenterDrop;
            new IncidentWorker_RaidEnemy().TryExecute(parms);
        }
    }
}
