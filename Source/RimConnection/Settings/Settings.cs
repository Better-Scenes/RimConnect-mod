using System.Text.RegularExpressions;
using RimConnection.API;
using RimConnection.Settings;
using RimWorld;
using UnityEngine;
using Verse;

namespace RimConnection
{
    public class RimConnectSettings : ModSettings
    {
        public static string[] validCommands;

#if !DEBUG
        public static string BASE_URL = "http://rimconnect-backend.herokuapp.com/";
#else
        public static string BASE_URL = "http://localhost:8080/";
#endif

        public static string secret = "";
        public static string token = "";
        public static bool initialiseSuccessful = false;

        bool showSecret = false;

        public static int silverAwardPoints = -1;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<string>(ref secret, "secret", "", true);
            Scribe_Values.Look(ref silverAwardPoints, "silverAwardPoints");

        }

        public void DoWindowContents(Rect rect)
        {
            Rect accountLinkHeader = new Rect(0, 32f, rect.width, 64f);
            Widgets.Label(accountLinkHeader, "<size=32>Account Link</size>");

            Rect statusGroup = new Rect(0, accountLinkHeader.y + accountLinkHeader.height + 10f, rect.width, 48f);

            GUI.BeginGroup(statusGroup);

            Rect statusLabel = new Rect(0, 0, 200f, 24f);
            Rect connectionButton = new Rect(200f + WidgetRow.LabelGap, 24f, 200f, 24f);
            Widgets.Label(statusLabel, "Status:");
            statusLabel.x += statusLabel.width + WidgetRow.LabelGap;
            if (initialiseSuccessful)
            {
                Widgets.Label(statusLabel, "<color=green>Connected!</color>");

                if (Widgets.ButtonText(connectionButton, "Reconnect"))
                {
                    ServerInitialise.Init();
                }
            }
            else
            {
                Widgets.Label(statusLabel, "<color=red>Disconnected</color>");

                if (Widgets.ButtonText(connectionButton, "Connect"))
                {
                    ServerInitialise.Init();
                }
            }

            GUI.EndGroup();

            Rect secretGroup = new Rect(0, statusGroup.y + statusGroup.height + 20f, rect.width, 72f);

            GUI.BeginGroup(secretGroup);

            Rect secretLabel = new Rect(0, 0, 200f, 24f);
            Rect pasteButton = new Rect(200f, 24f, 200f, 24f);
            Rect warningLabel = new Rect(0, 48, 400f, 24f);

            Widgets.Label(secretLabel, "Secret:");
            secretLabel.x = secretLabel.width + WidgetRow.LabelGap;
            
            if (showSecret)
            {
                secret = Widgets.TextField(secretLabel, secret);
            }
            else
            {
                Widgets.Label(secretLabel, new string('*', secret.Length));
            }

            secretLabel.x += secretLabel.width + WidgetRow.LabelGap;
            if (!showSecret && Widgets.ButtonText(secretLabel, "Show"))
            {
                showSecret = true;
            }
            else if (Widgets.ButtonText(secretLabel, "Hide"))
            {
                showSecret = false;
            }

            if (Widgets.ButtonText(pasteButton, "Paste from Clipboard"))
            {
                secret = GUIUtility.systemCopyBuffer;
            }

            Widgets.Label(warningLabel, "<color=red>Warning: Do not show your secret on stream!</color>");

            GUI.EndGroup();

            Rect loyaltyStoreHeader = new Rect(0, secretGroup.y + secretGroup.height + 10f, rect.width, 64f);
            Widgets.Label(loyaltyStoreHeader, "<size=32>Loyalty Settings</size>");

            Rect itemStoreGroup = new Rect(0, loyaltyStoreHeader.y + loyaltyStoreHeader.height + 10f, rect.width, 24f);

            if (CommandOptionListController.commandOptionList != null)
            {
                GUI.BeginGroup(itemStoreGroup);

                Rect itemLabel = new Rect(0, 0, 200f, 24f);
                Widgets.Label(itemLabel, "Loyalty Store Items:");

                itemLabel.x += itemLabel.width + WidgetRow.LabelGap;

                if (Widgets.ButtonText(itemLabel, "Items"))
                {
                    CommandOptionSettings window = new CommandOptionSettings();
                    Find.WindowStack.TryRemove(window.GetType());
                    Find.WindowStack.Add(window);
                }

                GUI.EndGroup();
            }
            else
            {
                Widgets.Label(itemStoreGroup, "Cannot edit items without proper connection to RimConnect Servers");
            }

            Rect silversPerGroup = new Rect(0, itemStoreGroup.y + itemStoreGroup.height + 10f, rect.width, 24f);

            GUI.BeginGroup(silversPerGroup);

            Rect silverLabel = new Rect(0, 0, 200f, 24f);
            Widgets.Label(silverLabel, "Silver per 2 minutes:");

            silverLabel.x += silverLabel.width + WidgetRow.LabelGap;

            string silverAwardPointsBuffer = silverAwardPoints.ToString();
            Widgets.TextFieldNumeric(silverLabel, ref silverAwardPoints, ref silverAwardPointsBuffer);

            silverLabel.x += silverLabel.width + WidgetRow.LabelGap;

            if (Widgets.ButtonText(silverLabel, "Update on Server"))
            {
                RimConnectAPI.PostConfig();
            }

            GUI.EndGroup();
        }
    }
}
