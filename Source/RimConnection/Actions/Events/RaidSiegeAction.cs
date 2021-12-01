using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidSiegeAction : Action
    {
        public RaidSiegeAction()
        {
            Name = "Raid (Siege)";
            Description = "Looks like they want to stick around";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);

            parms.forced = true;
            parms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeDrop;
            parms.raidStrategy = DefDatabase<RaidStrategyDef>.GetNamed("Siege");

            var raidWorker = new IncidentWorker_RaidEnemy();
            raidWorker.def = IncidentDef.Named("RaidEnemy");

            raidWorker.TryExecute(parms);
            AlertManager.BadEventNotification("{0} sent a siege!", boughtBy);
        }
    }
}
