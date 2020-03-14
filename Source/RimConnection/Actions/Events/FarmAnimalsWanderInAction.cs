using RimWorld;
using Verse;

namespace RimConnection
{
    class FarmAnimalsWanderInAction : Action
    {
        public FarmAnimalsWanderInAction()
        {
            name = "Farm animals join";
            description = "Let's just hope the pigs don't take over";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = 0;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var animalWorker = new IncidentWorker_FarmAnimalsWanderIn();
            animalWorker.def = IncidentDef.Named("FarmAnimalsWanderIn");
            animalWorker.TryExecute(parms);

            AlertManager.NormalEventNotification("Your viewers have sent some Farm Animals!");

        }
    }
}
