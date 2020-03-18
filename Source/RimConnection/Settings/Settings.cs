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

        //public static string BASE_URL = "https://rimconnect-dev.herokuapp.com/";
        public static string BASE_URL = "http://rimconnect-backend.herokuapp.com/";
        //public static string BASE_URL = "http://localhost:8080/";

        public static string secret = "";
        public static string token = "";
        public static bool initialiseSuccessful = false;

        bool showSecret = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<string>(ref secret, "secret", "", true);

        }

        public void DoWindowContents(Rect rect)
        {
            Rect accountLink = new Rect(0f, 32f, rect.width, 64f);
            Widgets.Label(accountLink, "<size=32>Account Link</size>");

            Widgets.DrawLineHorizontal(0f, 70f, rect.width);

            Rect label = new Rect(0f, 80f, 100f, 32f);
            Widgets.Label(label, "Status:");

            Rect status = new Rect(70f, 80f, 100f, 32f);
            Rect connectButton = new Rect(0f, 105f, 100f, 32f);
            if (initialiseSuccessful)
            {
                Widgets.Label(status, "<color=green>Connected!</color>");


                if (Widgets.ButtonText(connectButton, "Reconnect"))
                {
                    ServerInitialise.Init();
                }
            }
            else
            {
                Widgets.Label(status, "<color=red>Not Connected!</color>");

                if (Widgets.ButtonText(connectButton, "Connect"))
                {
                    ServerInitialise.Init();
                }
            }

            label.y = 170f;

            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.Label(label, "<b>Secret</b>: ");

            Rect secretInput = new Rect(70f, 170f, 200f, 32f);
            Rect hideSecretButton = new Rect(280f, 170f, 60f, 32f);

            if (showSecret)
            {
                secret = Widgets.TextField(secretInput, secret);
                
                if (Widgets.ButtonText(hideSecretButton, "Hide"))
                {
                    showSecret = false;
                }
            }
            else
            {
                Widgets.Label(secretInput, new string('*', secret.Length));

                if (Widgets.ButtonText(hideSecretButton, "Show"))
                {
                    showSecret = true;
                }
            }

            label.y = 200f;
            label.width = 300f;

            Widgets.Label(label, "<color=red>Warning: Do not show your secret on stream!</color>");

            Rect pasteButton = new Rect(0f, 225f, 200f, 32f);

            if (Widgets.ButtonText(pasteButton, "Paste Secret from Clipboard"))
            {
                secret = GUIUtility.systemCopyBuffer;
            }


            Rect loyaltyStore = new Rect(0f, 300f, rect.width, 64f);

            Widgets.Label(loyaltyStore, "<size=32>Loyalty Store</size>");

            Widgets.DrawLineHorizontal(0f, loyaltyStore.y + loyaltyStore.height, rect.width);

            loyaltyStore.y += loyaltyStore.height * 1.2f;
            loyaltyStore.height = 28f;
            loyaltyStore.width = loyaltyStore.width / 3f;

            if (CommandOptionListController.commandOptionList != null && Widgets.ButtonText(loyaltyStore, "Item Settings"))
            {
                CommandOptionSettings window = new CommandOptionSettings();
                Find.WindowStack.TryRemove(window.GetType());
                Find.WindowStack.Add(window);
            }

            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
}
