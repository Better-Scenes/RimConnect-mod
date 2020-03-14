using RimWorld;
using Verse;

namespace RimConnection
{
    class GoldenShowerAction : Action
    {
        public GoldenShowerAction()
        {
            name = "Golden Shower";
            description = "Gold rains from the skies";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var amountOfDrops = 100;

            for(int i = 0; i < amountOfDrops; i ++ )
            {
                DropPodManager.createDropFromDef(ThingDefOf.Gold, amount, name, description, false);
            }

            AlertManager.NormalEventNotification("Your viewers have sent a Golden Shower!");
        }
    }
}
