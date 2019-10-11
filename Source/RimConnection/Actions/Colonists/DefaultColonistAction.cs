namespace RimConnection
{
    class DefaultColonistAction : Action
    {
        public DefaultColonistAction()
        {
            this.name = "Generic Colonist";
            this.description = "You don't like me, but I like you. Maybe you could grow to like me?";
            this.canSpawnMultiple = true;
            this.category = "Colonists";
        }

        public override void execute(int amount)
        {
            PawnCreationManager.spawnDefaultColonist(amount, name, description);
        }
    }
}
