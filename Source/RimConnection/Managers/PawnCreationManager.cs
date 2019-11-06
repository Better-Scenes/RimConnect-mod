using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RimConnection
{
    class PawnCreationManager
    {
        private static List<Trait> badTraits = new List<Trait>(new Trait[] {
            new Trait(TraitDefOf.Beauty, -2),
            new Trait(TraitDefOf.Pyromaniac),
            new Trait(TraitDefOf.NaturalMood, -2),
            new Trait(TraitDefOf.Nerves, -2),
            new Trait(TraitDefOf.DrugDesire, 1),
            new Trait(TraitDefOf.Industriousness, -2),
            new Trait(TraitDefOf.SpeedOffset, -1),
        });

        private static List<Trait> goodTraits = new List<Trait>(new Trait[] {
            new Trait(TraitDefOf.Beauty, 2),
            new Trait(TraitDefOf.NaturalMood, 2),
            new Trait(TraitDefOf.Nerves, 2),
            new Trait(TraitDefOf.Industriousness, 2),
            new Trait(TraitDefOf.Cannibal),
            new Trait(TraitDefOf.SpeedOffset, 2),
        });

        public static List<Thing> generateDefaultColonists(int amount, Faction faction)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, faction);
                pawnList.Add(newPawn);
                i++;
            }

            return pawnList;
        }

        public static List<Thing> generateAwfulColonists(int amount)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                var randomTraits = badTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();

                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }
                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }

        public static List<Thing> generateGoodColonists(int amount)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                var randomTraits = goodTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();

                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }
                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }

    }
}
