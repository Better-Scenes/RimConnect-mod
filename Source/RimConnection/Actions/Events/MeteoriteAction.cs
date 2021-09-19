using RimWorld;
using Verse;

namespace RimConnection
{
    class MeteoriteAction : Action
    {
        public MeteoriteAction()
        {
            Name = "Meteorite";
            Description = "A chunk of asteroid";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.Misc, currentMap);
            parms.forced = true;

            for(int i = 0; i < amount; i++)
            {
                var beaverWorker = new IncidentWorker_MeteoriteImpact();
                beaverWorker.def = IncidentDef.Named("MeteoriteImpact");
                beaverWorker.TryExecute(parms);
            }

            AlertManager.NormalEventNotification("{0} dislodged a chunk of asteroid from orbit", boughtBy);

        }
    }
}
