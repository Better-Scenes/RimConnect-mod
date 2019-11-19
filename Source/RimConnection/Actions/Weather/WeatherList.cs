using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class WeatherList
    {
        public static List<IAction> weatherList = new List<IAction>
        {
            new ColdSnapAction(),
            new EclipseAction(),
            new FalloutAction(),
            new FlashstormAction(),
            new HeatWaveAction(),
            new SolarFlareAction(),
        };
    }
}
