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

        public override void Execute(int amount, string boughtBy)
        {
            string notificationMessage;
            if (boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has given you {amount} good colonist(s)";
                boughtBy = null;
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} good colonist(s)";
            }
            var pawnList = PawnCreationManager.generateGoodColonists(amount, boughtBy);

            DropPodManager.createDropOfThings(pawnList, "Good Colonist", notificationMessage);
        }
    }
}
