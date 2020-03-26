using Verse;

namespace RimConnection
{
    class GoodColonistAction : Action
    {

        public GoodColonistAction()
        {
            Name = "Good Colonist";
            Description = "The good, in The Good the bad and the ugly";
            ShouldShowAmount = true;
            Category = "Colonists";
            Prefix = "Spawn %amount%";
            CostSilverStore = 4000;
        }

        public override void Execute(int amount)
        {
            var pawnList = PawnCreationManager.generateGoodColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, Name, Description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
