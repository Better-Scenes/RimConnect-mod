using Verse;

namespace RimConnection
{
    class WorstColonistAction : Action
    {

        public WorstColonistAction()
        {
            Name = "Worst Colonist";
            Description = "For my friends Joeru707 and Myseenee";
            ShouldShowAmount = true;
            Category = "Colonists";
            Prefix = "Spawn %amount%";
            CostSilverStore = 2000;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var pawnList = PawnCreationManager.generateWorstColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, Name, Description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
