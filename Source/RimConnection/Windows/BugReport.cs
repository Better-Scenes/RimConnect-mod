using RimConnection.Settings;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnityEngine;
using Verse;

namespace RimConnection.Windows
{
    public class BugReport : Window
    {
        public BugReport(string feedback)
        {
            this.feedback = feedback;
            this.doCloseButton = true;
        }

        public override void DoWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);

            listing_Standard.Label("<color=red>We've Detected a Problem with RimConnect</color>");
            listing_Standard.Label(feedback);

            listing_Standard.Gap();

            if (listing_Standard.ButtonText("Copy Error"))
            {
                GUIUtility.systemCopyBuffer = CreateDetailedFeedback(feedback);
            }

            listing_Standard.End();
        }

        public static void CreateBugReport(string feedback)
        {
            if (Find.WindowStack == null)
            {
                return;
            }

            BugReport bugReport = new BugReport(feedback);
            Find.WindowStack.TryRemove(typeof(BugReport));
            Find.WindowStack.Add(bugReport);
        }

        static string CreateDetailedFeedback(string feedback)
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Error: {feedback}");
            stringBuilder.AppendLine($" occured at {currentEpochTime}.");                

            return stringBuilder.ToString();
        }

        string feedback;
    }
}
