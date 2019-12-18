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
            name = "Raid";
            description = "They don't look too happy";
            category = "Event";
            costSilverStore = -1;
            costBitStore = 100;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);

            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
            parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;

            var raidWorker = new IncidentWorker_RaidEnemy();
            raidWorker.def = IncidentDef.Named("RaidEnemy");
            raidWorker.TryExecute(parms);

            AlertManager.BadEventNotification("Your viewers have sent a Raid!");
        }
    }
}
