using Verse;
using Multiplayer.API;
using System;

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
                MP.RegisterSyncMethod(typeof(RimWorld.DropCellFinder), nameof(RimWorld.DropCellFinder.TradeDropSpot)).CancelIfAnyArgNull();
                MP.RegisterSyncMethod(typeof(RimWorld.TradeUtility), nameof(RimWorld.TradeUtility.SpawnDropPod)).CancelIfAnyArgNull();
                //MP.RegisterSyncMethod(typeof(AlertManager).GetMethod(nameof(AlertManager.BadEventNotification), new Type[] { typeof(string), typeof(IntVec3) }));
                //MP.RegisterSyncMethod(typeof(AlertManager).GetMethod(nameof(AlertManager.BadEventNotification), new Type[] { typeof(string) }));
                //MP.RegisterSyncMethod(typeof(AlertManager), nameof(AlertManager.NormalEventNotification));
                //MP.RegisterSyncMethod(typeof(AlertManager), nameof(AlertManager.ResourceDropNotification));
                //MP.RegisterSyncMethod(typeof(DropPodManager), nameof(DropPodManager.createDropFromDef));
                //MP.RegisterSyncMethod(typeof(DropPodManager), nameof(DropPodManager.createDropOfThings));
            }
        }
    }
}