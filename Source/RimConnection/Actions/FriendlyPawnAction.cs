namespace RimConnection
{
    class FriendlyPawnAction : Action
    {
        public FriendlyPawnAction()
        {
            this.name = "Friendly Pawn";
            this.description = "You don't like me, but I like you. Maybe you could grow to like me?";
        }

        public override void execute(int amount)
        {
            PawnCreationManager.createPawn(1);
        }
    }
}
