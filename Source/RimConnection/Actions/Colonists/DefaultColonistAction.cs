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
            var pawnList = PawnCreationManager.generateDefaultColonists(amount, Faction.OfPlayer);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, Name, Description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
