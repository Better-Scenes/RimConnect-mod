using RimWorld;

namespace RimConnection
{
    class GeothermalAction : Action
    {
        public GeothermalAction()
        {
            this.name = "Geothermal Generator";
            this.description = "Steamy hot steam";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDrop(ThingDefOf.GeothermalGenerator, 1);
        }
    }
}
