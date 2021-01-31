using Verse;
using RimWorld;

namespace RimConnection
{
    class DefaultColonistAction : Action
    {
        public DefaultColonistAction()
        {
            Name = "Generic Colonist";
            Description = "You don't like me, but I like you. Maybe you could grow to like me?";
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
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has given you {amount} normal colonist(s)";
                boughtBy = null;
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} normal colonist(s)";
            }
            var pawnList = PawnCreationManager.generateDefaultColonists(amount, boughtBy);

            DropPodManager.createDropOfThings(pawnList, "Normal Colonist", notificationMessage);
        }
    }
}
