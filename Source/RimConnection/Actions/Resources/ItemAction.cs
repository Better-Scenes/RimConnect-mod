﻿using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class ItemAction: Action, IAction
    {
        private string defName;
        private string defLabel;
        private ThingDef thingDef;

        public ItemAction(ThingDef itemDef, string category = "Item")
        {
            defName = itemDef.defName;
            defLabel = itemDef.label;
            name = defLabel;
            description = "What it says on the tin";
            shouldShowAmount = true;
            prefix = "Spawn %amount%";
            this.category = category;
            thingDef = itemDef;
            localCooldownMs = 30000;
            globalCooldownMs = 0;
            costSilverStore = (int)thingDef.BaseMarketValue;
            costBitStore = costBitStore;
    }

        public override void Execute(int amount)
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
            }
            else
            {
                // If Our item doesn't have stuff, is minifiable, or doesn't have quality
                // we can spawn it the old way with the itemdef
                var tempThing = ThingMaker.MakeThing(itemDef, GenStuff.DefaultStuffFor(itemDef));
                QualityCategory q = new QualityCategory();
                var tempThingQualityExists = tempThing.TryGetQuality(out q);
                if (!itemDef.MadeFromStuff && !itemDef.Minifiable && !tempThingQualityExists)
                {
                    DropPodManager.createDropFromDef(itemDef, amount, defLabel, $"Your viewers have given you {amount} {defLabel}s");
                } else
                {
                    List<Thing> thingsToSpawn = new List<Thing>();
                    for (var i = 0; i < amount; i++)
                    {
                        ThingDef itemStuff = null;
                        if(itemDef.MadeFromStuff)
                        {
                            itemStuff = GenStuff.RandomStuffByCommonalityFor(itemDef);
                        }
                    
                        var newThing = ThingMaker.MakeThing(itemDef, itemStuff);
                        tryAddQualityToThing(newThing);

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
            var command = new ValidCommand
            {
                name = name,
                description = description,
                category = category,
                prefix = prefix,
                actionHash = ActionHash(),
                localCooldownMs = localCooldownMs,
                globalCooldownMs = globalCooldownMs,
                costSilverStore = costSilverStore,
                costBitStore = costBitStore
            };
            return command;
        }

        private Thing tryAddQualityToThing(Thing thing)
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