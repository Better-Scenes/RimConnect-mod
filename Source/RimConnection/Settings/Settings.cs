using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using RimWorld;
using UnityEngine;
using Verse;

namespace RimConnection
{
    public class Settings : ModSettings
    {
        public static string[] validCommands;
        public static string BASE_URL = "http://localhost:8080/";
        public static string secret = "9419a81b1fa7562ec6ac7599ed550807";
        public static string token = "";

        public override void ExposeData()
        {
            var defaultValidCommands = new [] { "gold", "plasteel", "component" };
            Scribe_Values.Look<string>(ref secret, "secret", "9419a81b1fa7562ec6ac7599ed550807", true);

            Scribe_Values.Look<string[]>(ref validCommands, "validCommands", defaultValue: defaultValidCommands);
            base.ExposeData();
        }

        private static void DoSettingsWindowContents(Rect rect)
        {
            var labelRect = new Rect(20, 20, 20, 20);
            var inputRect = new Rect(20, 20, 20, 20);

            Widgets.Label(labelRect, "Secret: ");
            secret = Widgets.TextField(inputRect, secret, 16, new Regex("^[a-zA-Z0-9_]*$"));
        }
    }
}
