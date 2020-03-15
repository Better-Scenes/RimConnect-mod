using RimWorld;
using Verse;

namespace RimConnection
{
    class RaidSiegeAction : Action
    {
        public RaidSiegeAction()
        {
            name = "Raid (Siege)";
            description = "Looks like they want to stick around";
            category = "Event";
            prefix = "Trigger";
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
            AlertManager.BadEventNotification("Your viewers have sent a siege!");
        }
    }
}
