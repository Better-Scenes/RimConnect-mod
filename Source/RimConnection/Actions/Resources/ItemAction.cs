using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class ItemAction: IAction
    {
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; } = "Item";
        public string prefix { get; set; } = "Spawn %amount%";
        public bool shouldShowAmount { get; set; } = true;

        private string defName;
        private string defLabel;

        public ItemAction(ThingDef itemDef, string category = "Item")
        {
            defName = itemDef.defName;
            defLabel = itemDef.label;
            name = defLabel;
            description = "What it says on the tin";
            shouldShowAmount = true;
            prefix = "Spawn %amount%";
            this.category = category;
        }

        public void Execute(int amount)
        {
            var itemDef = DefDatabase<ThingDef>.GetNamed(defName);

            if(itemDef.race != null)
            {
                List<Thing> pawnList = new List<Thing>();
                for (int i = 0; i < amount; i++)
                {
                    pawnList.Add(PawnGenerator.GeneratePawn(itemDef.race.AnyPawnKind, null));
                }
                DropPodManager.createDropOfThings(pawnList, defLabel, $"Your viewers have given you {amount} {defLabel}s");
            } else if (itemDef.MadeFromStuff)
            {
                List<Thing> thingsToSpawn = new List<Thing>();
                for (int i = 0; i < amount; i++)
                {
                    ThingDef itemStuff = GenStuff.RandomStuffByCommonalityFor(itemDef);
                    var realThing = ThingMaker.MakeThing(itemDef, itemStuff);
                    QualityCategory q = new QualityCategory();

                    if (realThing.TryGetQuality(out q))
                    {
                        realThing.TryGetComp<CompQuality>().SetQuality(QualityUtility.GenerateQualityTraderItem(), ArtGenerationContext.Outsider);
                    }

                    thingsToSpawn.Add(realThing);
                }
                DropPodManager.createDropOfThings(thingsToSpawn, defLabel, $"Your viewers have given you {amount} {defLabel}s");
            }
            else
            {
                var testThing = ThingMaker.MakeThing(itemDef);
                QualityCategory q = new QualityCategory();

                if (testThing.TryGetQuality(out q))
                {
                    List<Thing> thingsToSpawn = new List<Thing>();
                    for (var i = 0; i < amount; i++)
                    {
                        var newThing = ThingMaker.MakeThing(itemDef);
                        newThing.TryGetComp<CompQuality>().SetQuality(QualityUtility.GenerateQualityTraderItem(), ArtGenerationContext.Outsider);
                        thingsToSpawn.Add(newThing);
                    }
                    DropPodManager.createDropOfThings(thingsToSpawn, defLabel, $"Your viewers have given you {amount} {defLabel}s");
                }
                else
                {
                    DropPodManager.createDropFromDef(itemDef, amount, defLabel, $"Your viewers have given you {amount} {defLabel}s", true);
                }
            }

        }


        public ValidCommand ToApiCall(int id)
        {
            var command = new ValidCommand
            {
                modId = id.ToString(),
                name = name,
                description = description,
                category = category,
                prefix = prefix
                
            };
            return command;
        }
    }
}
