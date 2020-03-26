using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidDropAction : Action
    {
        public RaidDropAction()
        {
            Name = "Raid (Drop)";
            Description = "They were inside the house the whole time!";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);

            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.CenterDrop;
            parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;

            var raidWorker = new IncidentWorker_RaidEnemy();
            raidWorker.def = IncidentDef.Named("RaidEnemy");


            raidWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Drop Raid!");

        }
    }
}
