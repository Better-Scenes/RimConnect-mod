using RimWorld;
using Verse;

namespace RimConnection
{
    class CargoPodAction : Action
    {
        public CargoPodAction()
        {
            name = "Cargo Pods";
            description = "A gift from above!";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = 0;
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
