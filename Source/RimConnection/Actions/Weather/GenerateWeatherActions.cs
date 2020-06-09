using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    public static class GenerateWeatherActions
    {

        public static List<IAction> GenerateWeatherDefActions()
        {
            IEnumerable<WeatherDef> allDefs = DefDatabase<WeatherDef>.AllDefs;
            List<IAction> allWeatherActions = allDefs.Select(weatherDef => CreateActionFromDef(weatherDef)).ToList();
            return allWeatherActions;
        }

        private static IAction CreateActionFromDef(WeatherDef weatherDef)
        {
            return new WeatherAction(weatherDef, "Weather");
        }
    }
}
