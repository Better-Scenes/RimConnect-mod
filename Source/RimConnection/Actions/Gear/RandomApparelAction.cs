using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RandomApparelAction : Action
    {
        public RandomApparelAction()
        {
            Name = "Random Apparel";
            Description = "Clothing, armour, it's all the same to me";
            ShouldShowAmount = true;
            Category = "Event";
            Prefix = "Spawn %amount%";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var apparelThingDefs = DefDatabase<ThingDef>.AllDefs.Where(def => { return def.IsApparel; });

            var randomApparelDefList = apparelThingDefs.ToList().TakeRandom(amount);

            var apparelThings = randomApparelDefList.Select(apparelDef => {
                var stuffForThing = GenStuff.RandomStuffFor(apparelDef);
                var thing = ThingMaker.MakeThing(apparelDef, stuffForThing);
                thing.SetForbidden(true);

                var thingQualityComp = thing.TryGetComp<CompQuality>();
                if (thingQualityComp != null)
                {
                    Array qualityValues = Enum.GetValues(typeof(QualityCategory));
                    QualityCategory randomQuality = (QualityCategory)qualityValues.GetValue(Rand.Range(0, qualityValues.Length));

                    thingQualityComp.SetQuality(randomQuality, ArtGenerationContext.Colony);
                }
                return thing;
            });
            
            DropPodManager.createDropOfThings(apparelThings.ToList(), Name, Description);
        }
    }
}
