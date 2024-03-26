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
        public static List<IAction> GenerateActionList()
        {
            actionList = new List<IAction>();

            actionList = actionList.Concat(ColonistList.colonistList)
            .Concat(EventList.eventList)
            .Concat(GearList.gearList)
            .Concat(OrbitalList.orbitalList)
            .Concat(GenerateAllItemActions.GenerateThingDefActions())
            .Concat(GenerateWeatherActions.GenerateWeatherDefActions())
            .Concat(GenerateGameConditionActions.GenerateGameConditionDefActions())
            .Concat(GenerateInspirationActions.GenerateInspirationDefActions())
            .Concat(GenerateMentalBreakActions.GenerateMentalBreakDefActions())
            .Concat(GenerateHeDiffActions.GenerateHeDiffDefActions())
            .Concat(GenerateTameAnimalActions.GeneratetameAnimalDefActions())
            .Concat(GenerateUntameAnimalActions.GenerateUntameAnimalDefActions())
            .Concat(CustomList.Actions)
            .ToList();

            return actionList;
        }

        // Make a dictionary lookup of all commands
        public static Dictionary<string, IAction> GenerateActionLookup()
        {
            actionLookup = new Dictionary<string, IAction>();
            GenerateActionList();

            actionList.ForEach((action) =>
            {
                try
                {
                    actionLookup.Add(action.GenerateActionHash(), action);
                } catch (Exception e)
                {
                    Log.Message($"{action.ActionHash} {action.Name} - {e.Message}");
                }
            });

            return actionLookup;
        }

        public static ValidCommandPayloadGenerator ActionListToApi()
        {
            // Make sure the list and dictionary are up to date
            GenerateActionLookup();
            List <ValidCommand> commands = actionList.Select(action => action.ToApiCall()).ToList();
            return new ValidCommandPayloadGenerator(commands);
        }
    }
}
