using RimWorld;
using Verse;

namespace RimConnection
{
    class BeaversAction : Action
    {
        public BeaversAction()
        {
            name = "Alpha Beavers";
            description = "Beavers might be cute, but they'll eat all your stuff";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = -1;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var beaverWorker = new IncidentWorker_Alphabeavers();
            beaverWorker.def = IncidentDef.Named("Alphabeavers");
            beaverWorker.TryExecute(parms);

            AlertManager.BadEventNotification("Your viewers have sent Alpha Beavers!");

        }
    }
}
