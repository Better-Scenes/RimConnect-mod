using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class ItemAction: Action, IAction
    {
        private string defName;
        private string defLabel;
        public ThingDef thingDef;

        public ItemAction(ThingDef itemDef, string category = "Item")
        {
            defName = itemDef.defName;
            defLabel = itemDef.label;
            Name = defLabel;
            Description = "What it says on the tin";
            ShouldShowAmount = true;
            Prefix = "Spawn %amount%";
            Category = category;
            thingDef = itemDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = (int)Math.Ceiling(thingDef.BaseMarketValue);
            BitStoreSKU = "";
    }

        public override void Execute(int amount)
        {
            ThingDef itemDef = DefDatabase<ThingDef>.GetNamed(defName);


            if(itemDef.race != null)
            {
                List<Thing> pawnList = new List<Thing>();
                for (int i = 0; i < amount; i++)
                {
                    pawnList.Add(PawnGenerator.GeneratePawn(itemDef.race.AnyPawnKind, null));
                }
                DropPodManager.createDropOfThings(pawnList, defLabel, $"Your viewers have given you {amount} {defLabel}s");
            }
            else
            {
                // If Our item doesn't have stuff, is minifiable, or doesn't have quality
                // we can spawn it the old way with the itemdef
                Thing thing = ThingMaker.MakeThing(itemDef, GenStuff.DefaultStuffFor(itemDef));
                QualityCategory q = new QualityCategory();
                bool thingHasQuality = thing.TryGetQuality(out q);
                if (!itemDef.MadeFromStuff && !itemDef.Minifiable && !thingHasQuality)
                {
                    DropPodManager.createDropFromDef(itemDef, amount, defLabel, $"Your viewers have given you {amount} {defLabel}s");
                } else
                {
                    List<Thing> thingsToSpawn = new List<Thing>();
                    for (int i = 0; i < amount; i++)
                    {
                        ThingDef itemStuff = null;
                        if(itemDef.MadeFromStuff)
                        {
                            itemStuff = GenStuff.RandomStuffByCommonalityFor(itemDef);
                        }
                    
                        Thing newThing = ThingMaker.MakeThing(itemDef, itemStuff);
                        TryAddQualityToThing(newThing);

                        if(itemDef.Minifiable)
                        {
                            newThing = newThing.MakeMinified();
                        }
                        thingsToSpawn.Add(newThing);
                    }
                    DropPodManager.createDropOfThings(thingsToSpawn, defLabel, $"Your viewers have given you {amount} {defLabel}s");
                }
            }
        
        }


        public ValidCommand ToApiCall(int id)
        {
            ValidCommand command = new ValidCommand
            {
                name = Name,
                description = Description,
                category = Category,
                prefix = Prefix,
                actionHash = GenerateActionHash($"{thingDef.description}{defName}"),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
            return command;
        }

        private Thing TryAddQualityToThing(Thing thing)
        {
            QualityCategory q = new QualityCategory();
            if (thing.TryGetQuality(out q))
            {
                thing.TryGetComp<CompQuality>().SetQuality(QualityUtility.GenerateQualityTraderItem(), ArtGenerationContext.Outsider);
            }

            return thing;
        }
    }
}