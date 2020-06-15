using Multiplayer.API;
using Verse;

namespace RimConnection.Patches
{
    [StaticConstructorOnStartup]
    public static class RCMPCompat
    {
        static RCMPCompat()
        {
            if (!MP.enabled) return;

            MP.RegisterAll();

            Log.Warning("RCMP enabled");
        }

        [SyncWorker]
        static void SyncCommand(SyncWorker sync, ref Command type)
        {
            if (sync.isWriting)
            {
                sync.Write(type.actionHash);
                sync.Write(type.amount);
                sync.Write(type.boughtBy);
            }
            else
            {
                Command command = new Command();
                command.actionHash = sync.Read<string>();
                command.amount = sync.Read<int>();
                command.boughtBy = sync.Read<string>();
                type = command;
            }
        }
    }
}