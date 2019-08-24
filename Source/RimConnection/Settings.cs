using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    public class Settings : ModSettings
    {
        public static string[] validCommands;
        public static string BASE_URL = "http://localhost:8080/";
        public static string username = "InfinitySamurai";

        public override void ExposeData()
        {
            var defaultValidCommands = new [] { "gold", "plasteel", "component" };
            Scribe_Values.Look<string[]>(ref validCommands, "validCommands", defaultValue: defaultValidCommands);
            base.ExposeData();
        }
    }
}
