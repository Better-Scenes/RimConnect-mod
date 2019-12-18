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
            name = "Raid (Drop)";
            description = "They were inside the house the whole time!";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = -1;
            costBitStore = 150;
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
