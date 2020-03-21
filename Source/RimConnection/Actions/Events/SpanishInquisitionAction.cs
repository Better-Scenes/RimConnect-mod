using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class SpanishInquisitionAction : Action
    {
        public SpanishInquisitionAction()
        {
            name = " Spanish Inquisition";
            description = "No one expects the Spanish Inquisition!";
            category = "Event";
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

            AlertManager.BadEventNotification("No one expects the Spanish Inquisition!");
        }
    }
}
