using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RandomWeaponAction : Action
    {
        public RandomWeaponAction()
        {
            Name = "Random Weapon";
            Description = "The best defence is a good offence";
            ShouldShowAmount = true;
            Category = "Event";
            Prefix = "Spawn %amount%";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var weaponThingDefs = DefDatabase<ThingDef>.AllDefs.Where(def => { return def.equipmentType == EquipmentType.Primary; });
            
            var randomWeaponDefList = weaponThingDefs.ToList().TakeRandom(amount);
            var weaponThings = randomWeaponDefList.Select(weaponDef => {
                var stuffForThing = GenStuff.RandomStuffFor(weaponDef);
                var thing =  ThingMaker.MakeThing(weaponDef, stuffForThing);
                thing.SetForbidden(true);

                var thingQualityComp = thing.TryGetComp<CompQuality>();
                if(thingQualityComp != null)
                {
                    Array qualityValues = Enum.GetValues(typeof(QualityCategory));
                    QualityCategory randomQuality = (QualityCategory)qualityValues.GetValue(Rand.Range(0, qualityValues.Length));

                    thingQualityComp.SetQuality(randomQuality, ArtGenerationContext.Colony);
                }
                return thing;
            });

            DropPodManager.createDropOfThings(weaponThings.ToList(), Name, Description);
        }
    }
}
