using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidAction : Action
    {
        public RaidAction()
        {
            Name = "Raid";
            Description = "They don't look too happy";
            Category = "Event";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);

            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
            parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;

            var raidWorker = new IncidentWorker_RaidEnemy();
            raidWorker.def = IncidentDef.Named("RaidEnemy");
            raidWorker.TryExecute(parms);

            AlertManager.BadEventNotification("{0} sent a Raid!", boughtBy);
        }
    }
}
