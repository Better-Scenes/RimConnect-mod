using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class BionicPlagueAction : Action
    {
        public BionicPlagueAction()
        {
            name = "Bionic Plague";
            description = "A bionic plague sweeps through all animals and leaves behind components";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = -1;
        }

        public override void Execute(int amount)
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

            AlertManager.NormalEventNotification("Every animal on the map has suddenly died to a bionic plague. You'll find they left behind some surprises!");
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
