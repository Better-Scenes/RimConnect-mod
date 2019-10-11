using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class WeatherList
    {
        public static List<Action> weatherList = new List<Action>
        {
            new WeatherCategoryAction(),
            new ColdSnapAction(),
            new EclipseAction(),
            new FalloutAction(),
            new FlashstormAction(),
            new HeaterAction(),
            new SolarFlareAction(),
        };
    }
}
