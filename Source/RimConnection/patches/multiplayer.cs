using RimWorld;
using Verse;
using Multiplayer.API;

namespace RimConnection
{
    class Multiplayer
    {
        [StaticConstructorOnStartup]
        public static class MultiplayerCompatibility
        {
            static MultiplayerCompatibility()
            {
                if (MP.enabled)
                {
                    MP.RegisterAll();
                }
            }
        }
    }
}