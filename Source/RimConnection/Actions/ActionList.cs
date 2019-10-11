using System.Collections.Generic;
using System.Linq;

namespace RimConnection
{
    public static class ActionList
    {
        // Bring all the lists together from the categories
        public static List<Action> actionList()
        {
            List <Action> actionList = new List<Action>();

            actionList.Concat(ColonistList.colonistList);
            actionList.Concat(EventList.eventList);
            actionList.Concat(GearList.gearList);
            actionList.Concat(ResourceList.resourceList);
            actionList.Concat(StructureList.structureList);
            actionList.Concat(WeatherList.weatherList);

            return actionList;
        }



        public static ValidCommandList ActionListToApi()
        {
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = actionList().Select((action, index) => action.toApiCall(index)).ToList<ValidCommand>();
            return validCommandList;
        }
    }
}
