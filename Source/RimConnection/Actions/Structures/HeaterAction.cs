using RimWorld;

namespace RimConnection
{
    class HeaterAction : Action
    {
        public HeaterAction()
        {
            name = "Heater";
            description = "It's cold outside";
            category = "Structures";
            shouldShowAmount = false;
            prefix = "Spawn";
        }

        public override void Execute(int amount)
        {
            DropPodManager.createDropFromDef(ThingDefOf.Heater, 1, name, description);
        }
    }
}
