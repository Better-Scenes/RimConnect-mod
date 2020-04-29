using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateGameConditionActions
    {

        public static List<IAction> GenerateGameConditionDefActions()
        {
            IEnumerable<GameConditionDef> allDefs = DefDatabase<GameConditionDef>.AllDefs;
            List<IAction> allConditionActions = allDefs.Select(conditionDef => CreateActionFromDef(conditionDef)).ToList();
            return allConditionActions;
        }

        private static IAction CreateActionFromDef(GameConditionDef conditionDef)
        {
            return new GameConditionAction(conditionDef, "Game Condition");
        }
    }
}
