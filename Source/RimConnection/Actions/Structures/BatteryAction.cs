using RimWorld;

namespace RimConnection
{
    class BatteryAction : Action
    {
        public BatteryAction()
        {
            name = "Battery";
            description = "Make sure those electrons don't escape and make them do your bidding";
            category = "Structures";
            shouldShowAmount = false;
            prefix = "Spawn";
        }

        public override void Execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.Battery, 1, name, description);
        }
    }
}
