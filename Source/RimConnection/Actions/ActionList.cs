using System.Collections.Generic;
using System.Linq;

namespace RimConnection
{
    public static class ActionList
    {
        // Add all your actions in here. If they aren't here, they won't be available
        public static List<Action> actionList = new List<Action> {
            new BatteryAction(),
            new FriendlyPawnAction(),
            new GoldAction(),
            new PlasteelAction(),
            new SolarPanelAction(),
            new FalloutAction()
            // Currently doesn't spawn correctly
            //new WindTurbineAction()
        };

        public static ValidCommandList ActionListToApi()
        {
            var nameSortedList = actionList.OrderBy(x => x.name);
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = nameSortedList.Select((action, index) => action.toApiCall(index)).ToList<ValidCommand>();
            return validCommandList;
        }

    }
}
