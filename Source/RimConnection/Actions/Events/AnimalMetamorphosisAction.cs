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
            Name = "Animal Metamorphosis";
            Description = "Turns out your entire map is covered in shapeshifters";
            Category = "Event";
        }

        public override void Execute(int amount, string boughtBy)
        {
            Map currentMap = Find.CurrentMap;
            List<Thing> things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Pawn);

            IEnumerable<PawnKindDef> allAnimalDefKinds = DefDatabase<PawnKindDef>.AllDefs.Where(delegate (PawnKindDef x)
           {
               return x.RaceProps.Animal;
           });

            List<Pawn> animals = things.Where(thing => thing is Pawn)
                  .Select(pawn => (Pawn)pawn)
                  .Where(pawn => pawn.AnimalOrWildMan())
                  .ToList();

            animals.ForEach(animal =>
            {
                IntVec3 animalPos = animal.Position;
                PawnKindDef newAnimal = allAnimalDefKinds.RandomElement();
                Pawn_RelationsTracker animalRelations = animal.relations;
                Faction animalFaction = animal.Faction;
                Name animalName = animal.Name;

                animal.Destroy();

                Pawn replacementAnimal = PawnGenerator.GeneratePawn(newAnimal, animalFaction);
                replacementAnimal.relations = animalRelations;

                if (animal.Faction == Faction.OfPlayer)
                {
                    replacementAnimal.Name = animalName;
                }

                GenSpawn.Spawn(replacementAnimal, animalPos, currentMap);
            });

            AlertManager.NormalEventNotification("{0} sent an animal metamorphosis.", boughtBy);
        }
    }
}
