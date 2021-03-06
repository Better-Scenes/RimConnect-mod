﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class WeatherAction: Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public WeatherDef weatherDef;

        public WeatherAction(WeatherDef _weatherDef, string category = "Weather")
        {
            defName = _weatherDef.defName;
            defLabel = _weatherDef.label;
            Name = defLabel;
            Description = _weatherDef.description;
            ShouldShowAmount = true;
            Prefix = "Trigger";
            Category = category;
            this.weatherDef = _weatherDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = 0;
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {
            WeatherDef WeatherDef = DefDatabase<WeatherDef>.GetNamed(defName);
            String notificationMessage;

            // I hope no viewer uses RC with the name "Poll"
            if(boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has triggered {defLabel}, hopefully it helps!";
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {defLabel} for your colony. Enjoy!!";
            }

            Map map = Find.CurrentMap;

            map.weatherManager.TransitionTo(WeatherDef);

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
                actionHash = GenerateActionHash($"{weatherDef.description}{defName}"),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
        }
    }
}