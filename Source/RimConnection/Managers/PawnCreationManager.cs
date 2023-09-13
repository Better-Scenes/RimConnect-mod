using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using UnityEngine;

namespace RimConnection
{
    class PawnCreationManager
    {
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
        });

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

                // Create a dictionary to store available traits for each trait type
                Dictionary<TraitDef, List<Trait>> availableTraits = new Dictionary<TraitDef, List<Trait>>();
                foreach (Trait trait in badTraits)
                {
                    if (!availableTraits.ContainsKey(trait.def))
                    {
                        availableTraits[trait.def] = new List<Trait>();
                    }
                    availableTraits[trait.def].Add(trait);
                }

                List<Trait> selectedTraits = new List<Trait>();

                // Randomly select 3 different trait types
                while (selectedTraits.Count < 3 && availableTraits.Count > 0)
                {
                    TraitDef randomTraitDef = availableTraits.Keys.RandomElement();
                    List<Trait> traitsForType = availableTraits[randomTraitDef];
                    Trait selectedTrait = traitsForType.RandomElement();
                    selectedTraits.Add(selectedTrait);
                    availableTraits.Remove(randomTraitDef);
                }

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

                // Create a dictionary to store available traits for each trait type
                Dictionary<TraitDef, List<Trait>> availableTraits = new Dictionary<TraitDef, List<Trait>>();
                foreach (Trait trait in goodTraits)
                {
                    if (!availableTraits.ContainsKey(trait.def))
                    {
                        availableTraits[trait.def] = new List<Trait>();
                    }
                    availableTraits[trait.def].Add(trait);
                }

                List<Trait> selectedTraits = new List<Trait>();

                // Randomly select 3 different trait types
                while (selectedTraits.Count < 3 && availableTraits.Count > 0)
                {
                    TraitDef randomTraitDef = availableTraits.Keys.RandomElement();
                    List<Trait> traitsForType = availableTraits[randomTraitDef];
                    Trait selectedTrait = traitsForType.RandomElement();
                    selectedTraits.Add(selectedTrait);
                    availableTraits.Remove(randomTraitDef);
                }

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

                // Create a dictionary to store available traits for each trait type
                Dictionary<TraitDef, List<Trait>> availableTraits = new Dictionary<TraitDef, List<Trait>>();
                foreach (Trait trait in badTraits)
                {
                    if (!availableTraits.ContainsKey(trait.def))
                    {
                        availableTraits[trait.def] = new List<Trait>();
                    }
                    availableTraits[trait.def].Add(trait);
                }

                List<Trait> selectedTraits = new List<Trait>();

                // Randomly select 5 different trait types
                while (selectedTraits.Count < 5 && availableTraits.Count > 0)
                {
                    TraitDef randomTraitDef = availableTraits.Keys.RandomElement();
                    List<Trait> traitsForType = availableTraits[randomTraitDef];
                    Trait selectedTrait = traitsForType.RandomElement();
                    selectedTraits.Add(selectedTrait);
                    availableTraits.Remove(randomTraitDef);
                }

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
