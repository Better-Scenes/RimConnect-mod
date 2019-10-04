using RimWorld;

namespace RimConnection
{
    class HeaterAction : Action
    {
        public HeaterAction()
        {
            this.name = "Heater";
            this.description = "It's cold outside";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.Heater, 1, name, description);
        }
    }
}
