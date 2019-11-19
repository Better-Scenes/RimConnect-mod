using RimWorld;

namespace RimConnection
{
    class CoolerAction : Action
    {
        public CoolerAction()
        {
            name = "Cooler";
            description = "Time to chill out";
            category = "Structures";
            shouldShowAmount = false;
            prefix = "Spawn %amount%";
        }

        public override void Execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.Cooler, 1, name, description);
        }
    }
}
