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
            string notificationMessage;
            if (boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has given you {amount} awful colonist(s).";
                boughtBy = null;
            } else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} awful colonist(s).";
            }
            var pawnList = PawnCreationManager.generateAwfulColonists(amount, boughtBy);

            DropPodManager.createDropOfThings(pawnList, "Awful Colonist", notificationMessage);
        }
    }
}
