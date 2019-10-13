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

        public static void spawnDefaultColonist(int amount, string title, string desc )
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);
                GenSpawn.Spawn(newPawn, spawnLocation, currentMap);
                i++;
            }
            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, title, desc);
            Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(spawnLocation, currentMap));

        }

        public static void SpawnAwfulColonist(int amount, string title, string desc)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                GenSpawn.Spawn(newPawn, spawnLocation, currentMap);

                var randomTraits = badTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();

                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                i++;
            }

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, title, desc);
            Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(spawnLocation, currentMap));
        }

        public static void SpawnGoodColonist(int amount, string title, string desc)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            int i = 0;
            while (i < amount)
            {
                var newPawn = Verse.PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                GenSpawn.Spawn(newPawn, spawnLocation, currentMap);

                var randomTraits = goodTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();

                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }
                i++;
            }

            string labelString = "RimConnectionFriendlyPawnLabel".Translate();
            string messageString = "RimConnectionFriendlyPawnMailBody".Translate(amount, title, desc);
            Find.LetterStack.ReceiveLetter(labelString, messageString, LetterDefOf.PositiveEvent, new TargetInfo(spawnLocation, currentMap));
        }

    }
}
