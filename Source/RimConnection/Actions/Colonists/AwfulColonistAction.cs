using Verse;

namespace RimConnection
{
    class AwfulColonistAction : Action
    {

        public AwfulColonistAction()
        {
            this.name = "Awful Colonist";
            this.description = "Well, they might be useful for parts....";
            this.canSpawnMultiple = true;
            this.category = "Colonists";
        }

        public override void execute(int amount)
        {
            var pawnList = PawnCreationManager.generateAwfulColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, name, description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
