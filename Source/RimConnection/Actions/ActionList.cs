using System.Collections.Generic;
using System.Linq;

using Verse;

namespace RimConnection
{
    public static class ActionList
    {
        // Bring all the lists together from the categories
        public static List<Action> actionList()
        {
            List <Action> actionList = new List<Action>();


            return actionList.Concat(ColonistList.colonistList)
            .Concat(EventList.eventList)
            .Concat(GearList.gearList)
            .Concat(ResourceList.resourceList)
            .Concat(StructureList.structureList)
            .Concat(WeatherList.weatherList).ToList();
        }



        public static ValidCommandList ActionListToApi()
        {
            actionList().ForEach(action => Log.Message(action.name));
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = actionList().Select((action, index) => action.toApiCall(index)).ToList();
            return validCommandList;
        }
    }
}
