using Verse;
using RimWorld;
using UnityEngine;

namespace RimConnection
{
    public static class AlertManager
    {
        private static LetterDef twitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("TwitchEvent");
        private static LetterDef badTwitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("DangerousTwitchEvent");

        private static void EventNotification(string label, string description, LetterDef letterDef, IntVec3? location)
        {
            var currentMap = Find.CurrentMap;

            if(location == null)
            {
                Find.LetterStack.ReceiveLetter( new TaggedString(label), new TaggedString(description), letterDef);
            } else
            {
                // Have to make sure that location isn't null otherwise compiler complains
                var newVector = location.GetValueOrDefault();
                Find.LetterStack.ReceiveLetter(new TaggedString(label), new TaggedString(description), letterDef, new LookTargets(newVector, currentMap));
            }
        }
        
        public static void BadEventNotification(string description)
        {
            EventNotification("Twitch Event", description, badTwitchEventLetterDef, null );
        }
        
        public static void BadEventNotification(string description, IntVec3 location)
        {
            EventNotification("Twitch Event", description, badTwitchEventLetterDef, location);
        }

        public static void BadEventNotification(string description, string boughtBy) 
        {
            EventNotification("Twitch Event", ParseNotificationMessage(description, boughtBy), badTwitchEventLetterDef, null);
        }

        public static void BadEventNotification(string description, IntVec3 location, string boughtBy) 
        {
            EventNotification("Twitch Event", ParseNotificationMessage(description, boughtBy), badTwitchEventLetterDef, location);
        }
        
        public static void NormalEventNotification(string description)
        {
            EventNotification("Twitch Event", description, twitchEventLetterDef, null);
        }
        
        public static void ResourceDropNotification(string description, IntVec3 location)
        {
            EventNotification("Twitch Drop", description, twitchEventLetterDef, location);
        }

        public static void NormalEventNotification(string description, string boughtBy)
        {
            EventNotification("Twitch Event", ParseNotificationMessage(description, boughtBy), twitchEventLetterDef, null);
        }

        public static void ResourceDropNotification(string description, IntVec3 location, string boughtBy)
        {
            EventNotification("Twitch Drop", ParseNotificationMessage(description, boughtBy), twitchEventLetterDef, location);
        }
        
        public static string ParseNotificationMessage(string message, string boughtBy) 
        {
            if (boughtBy == "Poll") { boughtBy = "Your twitch viewers"; }
            boughtBy = $"<color=#9147ff>{boughtBy}</color>"; 
            return string.Format(message, boughtBy);
        }

    }
}
