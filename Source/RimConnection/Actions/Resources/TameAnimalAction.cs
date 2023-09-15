using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class TameAnimalAction: Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public ThingDef thingDef;

        private int baseValueModifier = 2;

        public TameAnimalAction(ThingDef itemDef, string category = "Tame Animal")
        {
            defName = itemDef.defName;
            defLabel = itemDef.label;
            Name = $"Tame {itemDef.label}";
            Description = "A tame one!";
            ShouldShowAmount = true;
            Prefix = "Spawn %amount%";
            Category = category;
            thingDef = itemDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = (int)Math.Ceiling(thingDef.BaseMarketValue * baseValueModifier);
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {
            ThingDef itemDef = DefDatabase<ThingDef>.GetNamed(defName);
            String dropMessage;

            if (boughtBy == "Poll")
            {
                dropMessage = $"<color=#9147ff>By popular opinion</color>, your channel has gifted you {amount} tame {defLabel}s for your colony. Enjoy!!";
            }
            else
            {
                dropMessage = $"<color=#9147ff>{boughtBy}</color> purchased {amount} tame {defLabel}s for your colony. Enjoy!!";
            }

            List<Thing> pawnList = new List<Thing>();
            for (int i = 0; i < amount; i++)
            {
                Pawn newAnimal = PawnGenerator.GeneratePawn(itemDef.race.AnyPawnKind, Faction.OfPlayer);
                if(boughtBy != "Poll")
                {
                    newAnimal.Name = new NameSingle(boughtBy);
                }
                pawnList.Add(newAnimal);
            }

            DropPodManager.createDropOfThings(pawnList, defLabel, dropMessage);
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
    }
}