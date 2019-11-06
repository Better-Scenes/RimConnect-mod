using RimWorld;

namespace RimConnection
{
    class CoolerAction : Action
    {
        public CoolerAction()
        {
            this.name = "Cooler";
            this.description = "Time to chill out";
            this.category = "Structures";
        }

        public override void execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.Cooler, 1, name, description);
        }
    }
}
