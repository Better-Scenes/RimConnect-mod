using System.Collections.Generic;
using System.Linq;

namespace RimConnection
{
    public static class ActionList
    {
        public static List<IAction> allActionsList()
        {
            return new List<IAction>()
                .Concat(ColonistList.colonistList)
                .Concat(EventList.eventList)
                .Concat(GearList.gearList)
                .Concat(GenerateAllItemActions.GenerateThingDefActions())
                .Concat(StructureList.structureList)
                .Concat(WeatherList.weatherList).ToList();
        }

        // Bring all the lists together from the categories
        public static Dictionary<string, IAction> actionLookup()
        {
            Dictionary<string, IAction> actionLookup = new Dictionary<string, IAction>();

            allActionsList().ForEach(action =>
            {
                actionLookup.Add(action.ActionHash(), action);
            });

            return actionLookup;
        }

        public static ValidCommandList ActionListToApi()
        {
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = allActionsList().Select(action => action.ToApiCall()).ToList();
            return validCommandList;
        }
    }
}
