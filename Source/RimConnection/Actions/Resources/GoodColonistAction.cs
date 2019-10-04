namespace RimConnection
{
    class GoodColonistAction : Action
    {

        public GoodColonistAction()
        {
            this.name = "Good Colonist";
            this.description = "The good, in The Good the bad and the ugly";
            this.canSpawnMultiple = true;
        }

        public override void execute(int amount)
        {
            PawnCreationManager.SpawnGoodColonist(amount, name, description);
        }
    }
}
