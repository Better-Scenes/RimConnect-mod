using System.Text.RegularExpressions;
using RimConnection.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace RimConnection
{
    public class RimConnectSettings : ModSettings
    {
        public static string[] validCommands;

        //public static string BASE_URL = "http://rimconnect-backend.herokuapp.com/";
        public static string BASE_URL = "http://localhost:8080/";

        public static string secret = "";
        public static string token = "";
        public static bool initialiseSuccessful = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<string>(ref secret, "secret", "", true);

        }

        static int notificationFrames = 0;
        static string notification = "";

        public void DoWindowContents(Rect rect)
        {
            Listing_Standard settings = new Listing_Standard();
            settings.Begin(rect);

            float width = rect.width;
            settings.ColumnWidth = width * 0.7f;

            if (initialiseSuccessful)
            {
                settings.Label("<color=green>Connected</color>");
            }
            else
            {
                settings.Label("<color=red>Not Connected</color>");
            }

            secret = settings.TextEntryLabeled("Secret: ", secret);

            if (secret != "" && settings.ButtonText("Connect"))
            {
                var regexItem = new Regex("^[a-zA-Z0-9_]*$");

                if (regexItem.IsMatch(secret))
                {
                    var success = ServerInitialise.Init();
                    if (success)
                    {
                        notification = "<color=yellow>Reconnecting to RimConnect Services</color>";
                    }
                    else
                    {
                        notification = "<color=red>Failed to connect! Check your Debug Log</color>";
                    }
                }
                else
                {
                    notification = "<color=red>Invalid Secret</color>";
                }
                notificationFrames = 120;
            }

            if (notificationFrames > 0 && notification != "")
            {
                settings.Label(notification);
                notificationFrames--;
            }
            else if (notificationFrames == 0)
            {
                notification = "";
            }

            settings.NewColumn();
            settings.ColumnWidth = width * 0.25f;

            if (CommandOptionListController.commandOptionList != null && settings.ButtonText("Loyalty Store"))
            {
                Window itemSettings = new CommandOptionSettings();
                Find.WindowStack.TryRemove(itemSettings.GetType());
                Find.WindowStack.Add(itemSettings);
            }

            settings.End();
        }
    }
}
