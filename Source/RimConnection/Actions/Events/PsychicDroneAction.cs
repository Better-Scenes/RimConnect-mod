using RimWorld;
using Verse;

namespace RimConnection
{
    class PsychicDroneAction : Action
    {
        public PsychicDroneAction()
        {
            name = "Psychic Drone";
            description = "Can you hear that? Man that's annoying";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = 1000;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var worker = new IncidentWorker_PsychicDrone();
            worker.def = IncidentDef.Named("PsychicDrone");

            worker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Phsychic Drone");
        }
    }
}
