using Verse;
using RimWorld;

namespace RimConnection
{
    class DefaultColonistAction : Action
    {
        public DefaultColonistAction()
        {
            this.name = "Generic Colonist";
            this.description = "You don't like me, but I like you. Maybe you could grow to like me?";
            this.canSpawnMultiple = true;
            this.category = "Colonists";
        }

        public override void execute(int amount)
        {
            var pawnList = PawnCreationManager.generateDefaultColonists(amount, Faction.OfPlayer);

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, name, description);
            DropPodManager.createDropOfThings(pawnList, labelString, messageString);
        }
    }
}
