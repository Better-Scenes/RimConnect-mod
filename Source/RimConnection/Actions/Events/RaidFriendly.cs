using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidFriendly : Action
    {
        public RaidFriendly()
        {
            Name = "Call for Aid";
            Description = "Send some allies to wipe out your foes";
            Category = "Event";
        }

        public override void Execute(int amount, string boughtBy)
        {
            Map currentMap = Find.CurrentMap;
            

            IncidentParms parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.AllyAssistance, currentMap);

            parms.forced = true;
            //parms.raidArrivalMode = PawnsArrivalModeDefOf.CenterDrop;
            //parms.raidStrategy = RaidStrategyDefOf.ImmediateAttackFriendly;
            parms.raidArrivalModeForQuickMilitaryAid = true;
            parms.points = DiplomacyTuning.RequestedMilitaryAidPointsRange.RandomInRange;
            parms.faction = Find.FactionManager.RandomAlliedFaction();

            var raidWorker = new IncidentWorker_RaidFriendly();
            raidWorker.def = IncidentDef.Named("RaidFriendly");
            raidWorker.TryExecute(parms);

            AlertManager.NormalEventNotification("Your viewers have sent help from ");
        }
    }
}
