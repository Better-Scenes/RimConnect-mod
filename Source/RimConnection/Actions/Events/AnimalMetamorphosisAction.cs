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
            this.description = "Turns our your entire map is covered in shapeshifters";
            this.category = "Event";
        }

        public override void Execute(int amount)
        {
            Map currentMap = Find.CurrentMap;
            List<Thing> things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Pawn);

            var allAnimalDefKinds = DefDatabase<PawnKindDef>.AllDefs.Where(delegate (PawnKindDef x)
           {
               return x.RaceProps.Animal;
           });

            List<Pawn> animals = things.Where(thing => thing is Pawn)
                  .Select(pawn => (Pawn)pawn)
                  .Where(pawn => pawn.AnimalOrWildMan())
                  .ToList();

            Log.Message(animals.Count.ToString());

            animals.ForEach(animal =>
            {
                IntVec3 animalPos = animal.Position;
                PawnKindDef newAnimal = allAnimalDefKinds.RandomElement();
                var animalRelations = animal.relations;
                var animalFaction = animal.Faction;

                animal.Destroy();

                var replacementAnimal = PawnGenerator.GeneratePawn(newAnimal, animalFaction);
                replacementAnimal.relations = animalRelations;

                GenSpawn.Spawn(replacementAnimal, animalPos, currentMap);
            });

            AlertManager.NormalEventNotification("Your viewers have sent an Animal Metamorphosis");
        }
    }
}
