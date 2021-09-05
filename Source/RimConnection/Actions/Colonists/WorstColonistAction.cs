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
            string notificationMessage;
            if (boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has given you {amount} of the worst possible colonist(s)";
                boughtBy = null;
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} of the worst possible colonist(s)";
            }
            var pawnList = PawnCreationManager.generateWorstColonists(amount);

            DropPodManager.createDropOfThings(pawnList, "Worst Colonist", notificationMessage);
        }
    }
}
