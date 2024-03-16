using RimWorld;
using Verse;

namespace RimConnection
{
    class PsychicDroneAction : Action
    {
        public PsychicDroneAction()
        {
            Name = "Psychic Drone";
            Description = "Can you hear that? Man, that's annoying";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var worker = new IncidentWorker_PsychicDrone();
            worker.def = IncidentDef.Named("PsychicDrone");

            worker.TryExecute(parms);
            AlertManager.BadEventNotification("{0} sent a psychic drone.", boughtBy);
        }
    }
}
