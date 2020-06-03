using Multiplayer.API;
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

        [SyncMethod]
        public static List<Thing> generateDefaultColonists(int amount, Faction faction)
        {
            var currentMap = Find.CurrentMap;
            IntVec3 spawnLocation = DropCellFinder.TradeDropSpot(currentMap);

            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, faction);

                // mid range their skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(2, 7);
                });
                pawnList.Add(newPawn);
                i++;
            }

            return pawnList;
        }

        [SyncMethod]
        public static List<Thing> generateAwfulColonists(int amount)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                // fuck up their traits
                var randomTraits = badTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();
                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                // now fuck up their skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(0, 2);
                });

                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }

        [SyncMethod]
        public static List<Thing> generateGoodColonists(int amount)
        {
            List<Thing> pawnList = new List<Thing>();

            int i = 0;
            while (i < amount)
            {
                var newPawn = PawnGenerator.GeneratePawn(PawnKindDefOf.Colonist, Faction.OfPlayer);

                var randomTraits = goodTraits.InRandomOrder().Take(3);
                newPawn.story.traits.allTraits.Clear();

                // Make their traits great
                foreach (Trait trait in randomTraits)
                {
                    newPawn.story.traits.GainTrait(trait);
                }

                // Now give them good skills
                var pawnSkills = newPawn.skills.skills;
                pawnSkills.ForEach(skill =>
                {
                    skill.Level = Rand.Range(4, 10);
                });

                pawnList.Add(newPawn);
                i++;
            }
            return pawnList;
        }

    }
}
