using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Multiplayer.API;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class ItemAction: Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
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

        public override void Execute(int amount, string boughtBy)
        {
            ThingDef itemDef = DefDatabase<ThingDef>.GetNamed(defName);
            String dropMessage;

            // I hope no viewer uses RC with the name "Poll"
            if(boughtBy == "Poll")
            {
                dropMessage = $"<color=#9147ff>By popular opinion</color>, your channel has gifted you {amount} {defLabel}s for your colony. Enjoy!!";
            }
            else
            {
                dropMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} {defLabel}s for your colony. Enjoy!!";
            }

            if (itemDef.race != null)
            {
                List<Thing> pawnList = new List<Thing>();
                for (int i = 0; i < amount; i++)
                {
                    pawnList.Add(PawnGenerator.GeneratePawn(itemDef.race.AnyPawnKind, null));
                }
                DropPodManager.createDropOfThings(pawnList, defLabel, dropMessage);
            }
            else
            {
                Thing thing = ThingMaker.MakeThing(itemDef, GenStuff.DefaultStuffFor(itemDef));
                bool thingHasQuality = thing.TryGetQuality(out QualityCategory qualityCategory);
                if (itemDef.MadeFromStuff || itemDef.Minifiable || thingHasQuality)
                {
                    List<Thing> thingsToSpawn = new List<Thing>();
                    for (int i = 0; i < amount; i++)
                    {
                        ThingDef itemStuff = null;
                        if (itemDef.MadeFromStuff)
                        {
                            itemStuff = GenStuff.RandomStuffByCommonalityFor(itemDef);
                        }

                        Thing newThing = ThingMaker.MakeThing(itemDef, itemStuff);
                        TryAddQualityToThing(newThing);

                        if (itemDef.Minifiable)
                        {
                            newThing = newThing.MakeMinified();
                        }
                        thingsToSpawn.Add(newThing);
                    }
                    DropPodManager.createDropOfThings(thingsToSpawn, defLabel, dropMessage);
                }
                // If Our item doesn't have stuff, is minifiable, or doesn't have quality
                // we can spawn it the old way with the itemdef
                else
                {
                    DropPodManager.createDropFromDef(itemDef, amount, defLabel, dropMessage);
                }
            }
        
        }


        public ValidCommand ToApiCall(int id)
        {
            return new ValidCommand
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
        }

        private Thing TryAddQualityToThing(Thing thing)
        {
            if (thing.TryGetQuality(out QualityCategory qualityCategory))
            {
                thing.TryGetComp<CompQuality>().SetQuality(QualityUtility.GenerateQualityTraderItem(), ArtGenerationContext.Outsider);
            }

            return thing;
        }
    }
}