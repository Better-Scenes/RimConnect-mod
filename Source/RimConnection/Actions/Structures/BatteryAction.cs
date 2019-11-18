using RimWorld;

namespace RimConnection
{
    class BatteryAction : Action
    {
        public BatteryAction()
        {
            this.name = "Battery";
            this.description = "Make sure those electrons don't escape and make them do your bidding";
            this.category = "Structures";
        }

        public override void Execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.Battery, 1, name, description);
        }
    }
}
