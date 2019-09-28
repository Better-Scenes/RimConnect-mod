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

        public static string secret = "";
        public static string token = "";
        public static bool initialiseSuccessful = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<string>(ref secret, "secret", "", true);

        }


        public static void DoSettingsWindowContents(Rect rect)
        {
            GUI.BeginGroup(new Rect(0, 60, 600, 200));
            var labelRect = new Rect(0, 40, 50, 20);
            var inputRect = new Rect(70, 40, 300, 20);

            Widgets.Label(labelRect, "Secret: ");
            secret = Widgets.TextField(inputRect, secret, 16, new Regex("^[a-zA-Z0-9_]*$")).Trim();
            if (Widgets.ButtonText(new Rect(380, 40, 100, 20), "Paste"))
            {
                secret = GUIUtility.systemCopyBuffer;
            }
            if (Widgets.ButtonText(new Rect(70, 70, 100, 20), "Connect"))
            {
                var success = ServerInitialise.Init();
                if(success)
                {
                    Messages.Message("Connected!", MessageTypeDefOf.PositiveEvent);

                } else
                {
                    Messages.Message("Failed to connect! Check your debug log", MessageTypeDefOf.NegativeEvent);
                }
            }
                
        }
    }
}
