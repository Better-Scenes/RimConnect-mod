using RimWorld;
using Verse;

namespace RimConnection
{
    class BeaversAction : Action
    {
        public BeaversAction()
        {
            Name = "Alpha Beavers";
            Description = "Beavers might be cute, but they'll eat all your stuff";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var beaverWorker = new IncidentWorker_Alphabeavers();
            beaverWorker.def = IncidentDef.Named("Alphabeavers");
            beaverWorker.TryExecute(parms);

            if (boughtBy == "Poll") { boughtBy = "Your twitch viewers"; }
            string notificationMessage = $"<color=#9147ff>{boughtBy}</color> has sent Alpha Beavers!";

            AlertManager.BadEventNotification(notificationMessage);

        }
    }
}
