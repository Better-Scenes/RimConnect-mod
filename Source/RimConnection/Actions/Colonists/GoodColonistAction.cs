using Verse;

namespace RimConnection
{
    class GoodColonistAction : Action
    {

        public GoodColonistAction()
        {
            name = "Good Colonist";
            description = "The good, in The Good the bad and the ugly";
            shouldShowAmount = true;
            category = "Colonists";
            prefix = "Spawn %amount%";
            costSilverStore = 8000;
        }

        public override void Execute(int amount)
        {
            var pawnList = PawnCreationManager.generateGoodColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, name, description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
