using Verse;

namespace RimConnection
{
    class AwfulColonistAction : Action
    {

        public AwfulColonistAction()
        {
            Name = "Awful Colonist";
            Description = "Well, they might be useful for parts....";
            ShouldShowAmount = true;
            Category = "Colonists";
            Prefix = "Spawn %amount%";
            CostSilverStore = 1000;
    }

        public override void Execute(int amount, string boughtBy)
        {
            var pawnList = PawnCreationManager.generateAwfulColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, Name, Description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
