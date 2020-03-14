using RimWorld;
using Verse;

namespace RimConnection
{
    class AnimalSelfTameAction : Action
    {
        public AnimalSelfTameAction()
        {
            name = "Animal Self Tame";
            description = "You don't get to choose, hope it was a good one";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = 0;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_SelfTame().TryExecute(parms);
            AlertManager.NormalEventNotification("Your viewers have tamed some animals!");
        }
    }
}
