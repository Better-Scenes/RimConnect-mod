using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AnimalMetamorphosisAction : Action
    {
        public AnimalMetamorphosisAction()
        {
            this.name = "Animal Metamorphosis";
            this.description = "Turn our your entire map is covered in shapeshifters";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            var things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Pawn);

            var allAnimalDefs = DefDatabase<ThingDef>.AllDefs.Where(def => def.race != null && def.race.Animal ).ToList();

            var animals = things.Where(thing => thing is Pawn)
                  .Select(pawn => (Pawn)pawn)
                  .Where(pawn => pawn.def.race.Animal)
                  .ToList();

            animals.ForEach(animal =>
            {
                var animalPos = animal.Position;
                Log.Message(allAnimalDefs.Count.ToString());
                var newAnimal = allAnimalDefs.RandomElement();
                animal.Destroy();

                GenSpawn.Spawn(newAnimal, animalPos, currentMap);
            });

            Find.LetterStack.ReceiveLetter("Twitch Event", "To your great surprise, every animal has morphed into something else", LetterDefOf.NeutralEvent);
        }
    }
}
