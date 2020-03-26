using RimWorld;
using Verse;

namespace RimConnection
{
    class CargoPodAction : Action
    {
        public CargoPodAction()
        {
            Name = "Cargo Pods";
            Description = "A gift from above!";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var podWorker = new IncidentWorker_ResourcePodCrash();
            podWorker.def = IncidentDef.Named("ResourcePodCrash");
            podWorker.TryExecute(parms);
            AlertManager.NormalEventNotification("Your viewers have sent a Cargo Pod");

        }
    }
}
