using Verse;
using RimWorld;
using UnityEngine;

namespace RimConnection
{
    static class AlertManager
    {
        private static LetterDef twitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("TwitchEvent");
        private static LetterDef badTwitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("DangerousTwitchEvent");

        private static void EventNotification(string label, string description, LetterDef letterDef, IntVec3? location)
        {
            var currentMap = Find.CurrentMap;

            if(location == null)
            {
                Find.LetterStack.ReceiveLetter(label, description, letterDef);
            } else
            {
                // Have to make sure that location isn't null otherwise compiler complains
                var newVector = location.GetValueOrDefault();
                Find.LetterStack.ReceiveLetter(label, description, letterDef, new LookTargets(newVector, currentMap));
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

        public static void NormalEventNotification(string description)
        {
            EventNotification("Twitch Event", description, twitchEventLetterDef, null);
        }

        public static void ResourceDropNotification(string description, IntVec3 location)
        {
            EventNotification("Twitch Drop", description, twitchEventLetterDef, location);
        }
    }
}
