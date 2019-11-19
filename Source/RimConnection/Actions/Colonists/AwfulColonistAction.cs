using Verse;

namespace RimConnection
{
    class AwfulColonistAction : Action
    {

        public AwfulColonistAction()
        {
            name = "Awful Colonist";
            description = "Well, they might be useful for parts....";
            shouldShowAmount = true;
            category = "Colonists";
            prefix = "Spawn %amount%";
        }

        public override void Execute(int amount)
        {
            var pawnList = PawnCreationManager.generateAwfulColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, name, description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
