using Multiplayer.API;
using Verse;

namespace RimConnection
{
    [StaticConstructorOnStartup]
    public static class MultiplayerCompatibility
    {
        static MultiplayerCompatibility()
        {
            if (MP.enabled)
            {
                MP.RegisterAll();
                //MP.RegisterSyncMethod(typeof(RimWorld.DropCellFinder), nameof(RimWorld.DropCellFinder.TradeDropSpot)).CancelIfAnyArgNull();
                //MP.RegisterSyncMethod(typeof(RimWorld.TradeUtility), nameof(RimWorld.TradeUtility.SpawnDropPod)).CancelIfAnyArgNull();

            }
        }
    }
}