using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class BionicPlagueAction : Action
    {
        public BionicPlagueAction()
        {
            Name = "Bionic Plague";
            Description = "A bionic plague sweeps through all animals and leaves behind components";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;
            var animals = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Pawn).Where(nonBondedAnimalPredicate).ToList();

            animals.ForEach(animal =>
            {
                var position = animal.Position;
                animal.Kill();

                var componentToDrop = ThingMaker.MakeThing(ThingDefOf.ComponentIndustrial);
                componentToDrop.stackCount = Rand.Range(1, 3);
                Thing outThing = new Thing();
                GenThing.TryDropAndSetForbidden(componentToDrop, position, currentMap, ThingPlaceMode.Direct, out outThing, true);
            });

            if (boughtBy == "Poll") { boughtBy = "Twitch viewers"; }
            string notificationMessage = $"(<color=#9147ff>{boughtBy}</color>) Every animal on the map has suddenly died to a bionic plague. You'll find they left behind some surprises!";

            AlertManager.NormalEventNotification(notificationMessage);
        }

        // Find all pawns that are an animal and that don't have a relation to a pawn
        private bool nonBondedAnimalPredicate(Thing pawn)
        {
            if (pawn.def.race.Animal)
            {
                var animalPawn = (Pawn)pawn;
                return !animalPawn.relations.DirectRelations.Any(relation => relation.def == PawnRelationDefOf.Bond);
            }

            return false;
        }
    }
}
