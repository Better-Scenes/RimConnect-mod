using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class GameConditionAction: Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public GameConditionDef thingDef;

        public GameConditionAction(GameConditionDef ConditionDef, string category = "Game Condition")
        {
            defName = ConditionDef.defName;
            defLabel = ConditionDef.label;
            Name = defLabel;
            Description = ConditionDef.description;
            ShouldShowAmount = true;
            Prefix = "Trigger";
            Category = category;
            thingDef = ConditionDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = 0;
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {
            GameConditionDef conditionDef = DefDatabase<GameConditionDef>.GetNamed(defName);
            String notificationMessage;

            // I hope no viewer uses RC with the name "Poll"
            if(boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has triggered {defLabel} for a whole day! Let's hope you get out the other side";
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {defLabel} for a whole day! Let's hope you get out the other side";
            }

            Map map = Find.CurrentMap;

            GameCondition gameCondition = GameConditionMaker.MakeCondition(conditionDef, 60000);
            map.gameConditionManager.RegisterCondition(gameCondition);

            //map.weatherManager.TransitionTo(WeatherDef);

            AlertManager.NormalEventNotification(notificationMessage);
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