using Multiplayer.API;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Verse;

namespace RimConnection
{
    class ServerPoller : GameComponent
    {
        static DateTime lastGETRequest = DateTime.UtcNow;
        static readonly TimeSpan timeBetweenRequests = TimeSpan.FromSeconds(30d);
        static ConcurrentQueue<Command> commandQueue = new ConcurrentQueue<Command>();

        private DateTime previousDateTime;

        public ServerPoller(Game game)
        {
        }

        public override void FinalizeInit()
        {
            previousDateTime = DateTime.Now;
        }

        public override void GameComponentTick()
        {
            // Only do this stuff if the mod successfully connected to the server
            if (RimConnectSettings.initialiseSuccessful)
            {
                if (DateTime.UtcNow - lastGETRequest > timeBetweenRequests)
                {
                    lastGETRequest = DateTime.UtcNow;
                    ServerChecker();
                }
            }

            if (commandQueue.TryDequeue(out Command command))
            {
                IAction action = ActionList.actionLookup[command.actionHash];
                action.Execute(command.amount, command.boughtBy);
                Find.TickManager.slower.SignalForceNormalSpeedShort();
            }
        }

        public static async void ServerChecker()
        {
            await Task.Run(() =>
                {
                    List<Command> commands = RimConnectAPI.GetCommands();

                    foreach (Command command in commands)
                    {
                        addCommandToQueue(command);
                    }
                });

            return;
        }

        [SyncMethod]
        public static void addCommandToQueue(Command command)
        {
            commandQueue.Enqueue(command);
        }
    }
}
