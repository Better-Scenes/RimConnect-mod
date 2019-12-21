using System;
using System.Collections.Generic;
using System.Linq;

using Verse;

namespace RimConnection
{
    public static class ActionList
    {
        public static List<IAction> actionList;
        public static Dictionary<string, IAction> actionLookup;

        // Bring all the lists together from the categories
        public static List<IAction> generateActionList()
        {
            actionList = new List<IAction>();

            actionList =  actionList.Concat(ColonistList.colonistList)
            .Concat(EventList.eventList)
            .Concat(GearList.gearList)
            .Concat(GenerateAllItemActions.GenerateThingDefActions())
            .Concat(WeatherList.weatherList).ToList();

            return actionList;
        }

        // Make a dictionary lookup of all commands
        public static Dictionary<string, IAction> generateActionLookup()
        {
            actionLookup = new Dictionary<string, IAction>();
            generateActionList();

            actionList.ForEach((action) =>
            {
                try
                {
                    actionLookup.Add(action.GenerateActionHash(), action);
                } catch (Exception e)
                {
                    Log.Message(e.Message);
                    Log.Message($"{action.actionHash} {action.name}");
                }
            });

            return actionLookup;
        }

        public static ValidCommandList ActionListToApi()
        {
            // Make sure the list and dictionary are up to date
            generateActionLookup();

            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = actionList.Select(action => action.ToApiCall()).ToList();
            return validCommandList;
        }
    }
}
