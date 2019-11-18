using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidSiegeAction : Action
    {
        public RaidSiegeAction()
        {
            this.name = "Raid (Siege)";
            this.description = "Looks like they want to stick around";
            this.category = "Event";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);

            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeDrop;
            parms.raidStrategy = DefDatabase<RaidStrategyDef>.GetNamed("Siege");

            var raidWorker = new IncidentWorker_RaidEnemy();
            raidWorker.def = IncidentDef.Named("RaidEnemy");


            raidWorker.TryExecute(parms);
        }
    }
}
