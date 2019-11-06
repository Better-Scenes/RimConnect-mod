using Verse;

namespace RimConnection
{
    class GoodColonistAction : Action
    {

        public GoodColonistAction()
        {
            this.name = "Good Colonist";
            this.description = "The good, in The Good the bad and the ugly";
            this.canSpawnMultiple = true;
            this.category = "Colonists";
        }

        public override void execute(int amount)
        {
            var pawnList = PawnCreationManager.generateGoodColonists(amount);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, name, description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
