using System.Collections.Generic;
using System.Linq;

namespace RimConnection
{
    public static class ActionList
    {
        // Bring all the lists together from the categories
        public static List<IAction> actionList()
        {
            List<IAction> actionList = new List<IAction>();


            return actionList.Concat(ColonistList.colonistList)
            .Concat(EventList.eventList)
            .Concat(GearList.gearList)
            .Concat(GenerateAllItemActions.GenerateThingDefActions())
            .Concat(WeatherList.weatherList).ToList();
        }



        public static ValidCommandList ActionListToApi()
        {
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = actionList().Select((action, index) => action.ToApiCall(index)).ToList();
            return validCommandList;
        }
    }
}
