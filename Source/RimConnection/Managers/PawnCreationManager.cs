using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using UnityEngine;

namespace RimConnection
{
    class PawnCreationManager
    {
        private static Dictionary<TraitDef, List<Trait>> badTraitDict;
        private static Dictionary<TraitDef, List<Trait>> goodTraitDict;

        private static List<Trait> badTraits = new List<Trait>(new Trait[] {
            new Trait(TraitDefOf.Beauty, -2),
            new Trait(TraitDefOf.NaturalMood, -2),
            new Trait(TraitDefOf.Nerves, -2),
            new Trait(TraitDefOf.Industriousness, -2),
            new Trait(TraitDefOf.Beauty, -1),
            new Trait(TraitDefOf.NaturalMood, -1),
            new Trait(TraitDefOf.Nerves, -1),
            new Trait(TraitDefOf.Industriousness, -1),
            new Trait(TraitDefOf.SpeedOffset, -1),
            new Trait(TraitDefOf.DrugDesire, 1),
            new Trait(TraitDefOf.DrugDesire, 2),
            new Trait(TraitDefOf.Pyromaniac),
            new Trait(TraitDefOf.Wimp),
            new Trait(TraitDefOf.Greedy),
            new Trait(TraitDefOf.Jealous),
            new Trait(TraitDefOf.Abrasive),
            new Trait(TraitDefOf.AnnoyingVoice),
            new Trait(TraitDefOf.CreepyBreathing),
            new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), -1),
            new Trait(DefDatabase<TraitDef>.GetNamed("SlowLearner")),
        });

        private static List<Trait> goodTraits = new List<Trait>(new Trait[] {
            new Trait(TraitDefOf.Beauty, 1),
            new Trait(TraitDefOf.NaturalMood, 1),
            new Trait(TraitDefOf.Nerves, 1),
            new Trait(TraitDefOf.Industriousness, 1),
            new Trait(TraitDefOf.SpeedOffset, 1),
            new Trait(TraitDefOf.Beauty, 2),
            new Trait(TraitDefOf.NaturalMood, 2),
            new Trait(TraitDefOf.Nerves, 2),
            new Trait(TraitDefOf.Industriousness, 2),
            new Trait(TraitDefOf.SpeedOffset, 2),
            new Trait(TraitDefOf.Cannibal),
            new Trait(TraitDefOf.GreatMemory),
            new Trait(TraitDefOf.Tough),
            new Trait(DefDatabase<TraitDef>.GetNamed("Immunity"), 1),
            new Trait(DefDatabase<TraitDef>.GetNamed("FastLearner")),
            new Trait(TraitDefOf.Kind),
            new Trait(DefDatabase<TraitDef>.GetNamed("Nimble")),
            new Trait(DefDatabase<TraitDef>.GetNamed("QuickSleeper")),
            new Trait(DefDatabase<TraitDef>.GetNamed("NightOwl")),
        });

        private static Dictionary<TraitDef, List<Trait>> InitializeTraitDictionary(List<Trait> traits)
        {
            Dictionary<TraitDef, List<Trait>> traitDict = new Dictionary<TraitDef, List<Trait>>();
            foreach (Trait trait in traits)
            {
                if (!traitDict.ContainsKey(trait.def))
                {
                    traitDict[trait.def] = new List<Trait>();
                }
                traitDict[trait.def].Add(trait);
            }
            return traitDict;
        }

        static PawnCreationManager()
        {
            // Initialize the good and bad trait dictionaries
            goodTraitDict = InitializeTraitDictionary(goodTraits);
            badTraitDict = InitializeTraitDictionary(badTraits);
        }

        private static List<Trait> SelectRandomTraits(Dictionary<TraitDef, List<Trait>> traitDictionary, int numTraitsToSelect)
        {
            List<Trait> selectedTraits = new List<Trait>();

            // Create a new copy of the trait dictionary for each colonist
            Dictionary<TraitDef, List<Trait>> traitDictionaryCopy = new Dictionary<TraitDef, List<Trait>>(traitDictionary);


            // Randomly select traits until the desired number is reached or no more traits are available
            while (selectedTraits.Count < numTraitsToSelect && traitDictionaryCopy.Count > 0)
            {
                TraitDef randomTraitDef = traitDictionaryCopy.Keys.RandomElement();
                List<Trait> traitsForType = traitDictionaryCopy[randomTraitDef];
                Trait selectedTrait = traitsForType.RandomElement();
                selectedTraits.Add(selectedTrait);
                traitDictionaryCopy.Remove(randomTraitDef);
            }

            return selectedTraits;
        }

        public static List<Thing> generateDefaultColonists(int amount, string name = null)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist);
                newPawn.SetFaction(Faction.OfPlayer);

                // mid range their skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(2, 7);
                });

                if (name != null && newPawn.Name is NameTriple nameTriple)
                {
                    newPawn.Name = new NameTriple(nameTriple.First, name, nameTriple.Last);
                }

                pawnList.Add(newPawn);
                i++;
            }

            return pawnList;
        }

        public static List<Thing> generateAwfulColonists(int amount, string name = null)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist);
                newPawn.SetFaction(Faction.OfPlayer);

                // Generate 3 bad traits for the colonist
                List<Trait> selectedTraits = SelectRandomTraits(badTraitDict, 3);

                newPawn.story.traits.allTraits.Clear();
                // Add the selected traits to the pawn
                foreach (Trait trait in selectedTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                // now fuck up their skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(0, 2);
                });

                if (name != null && newPawn.Name is NameTriple nameTriple)
                {
                    newPawn.Name = new NameTriple(nameTriple.First, name, nameTriple.Last);
                }

                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }

        public static List<Thing> generateGoodColonists(int amount, string name = null)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist);
                newPawn.SetFaction(Faction.OfPlayer);

                // Generate 3 good traits for the colonist
                List<Trait> selectedTraits = SelectRandomTraits(goodTraitDict, 3);

                newPawn.story.traits.allTraits.Clear();
                // Add the selected traits to the pawn
                foreach (Trait trait in selectedTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                // Now give them good skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(4, 10);
                });

                if (name != null && newPawn.Name is NameTriple nameTriple)
                {
                    newPawn.Name = new NameTriple(nameTriple.First, name, nameTriple.Last);
                }

                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }


        public static List<Thing> generateWorstColonists(int amount)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist);
                newPawn.SetFaction(Faction.OfPlayer);

                if (newPawn.gender == Gender.Male)
                {
                    newPawn.Name = new NameTriple("Worst", "Joeru", "Ever");
                }
                else if (newPawn.gender == Gender.Female)
                {
                    newPawn.Name = new NameTriple("Worst", "Myseenee", "Ever");
                }

                // Generate 5 bad traits for the colonist
                List<Trait> selectedTraits = SelectRandomTraits(badTraitDict, 5);

                newPawn.story.traits.allTraits.Clear();
                // Add the selected traits to the pawn
                foreach (Trait trait in selectedTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = 0;
                    skill.passion = Passion.None;
                });

                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }
    }
}
