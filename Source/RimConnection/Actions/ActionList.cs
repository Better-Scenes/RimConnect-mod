using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class ActionList
    {
        public static List<Action> actionList = new List<Action> {
            new GoldAction(),
            new PlasteelAction()
        };

        public static ValidCommandList ActionListToApi()
        {
            ValidCommandList validCommandList = new ValidCommandList();
            validCommandList.validCommands = actionList.Select((action, index) => action.toApiCall(index)).ToList<ValidCommand>();
            return validCommandList;
        }

    }
}
