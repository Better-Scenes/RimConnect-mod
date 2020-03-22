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
        static readonly TimeSpan waitTimeSeconds = TimeSpan.FromSeconds(30);
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
                if (DateTime.UtcNow - lastGETRequest > waitTimeSeconds)
                {
                    lastGETRequest = DateTime.UtcNow;
                    serverChecker();
                }
            }

            if (commandQueue.Count > 0)
            {
                commandQueue.TryDequeue(out Command command);

                IAction action = ActionList.actionLookup[command.actionHash];
                action.Execute(command.amount);
                Find.TickManager.slower.SignalForceNormalSpeedShort();
            }
        }

        public static async void serverChecker()
        {
            await Task.Run(() =>
                {
                    var commands = RimConnectAPI.GetCommands();

                    foreach (var command in commands)
                    {
                        commandQueue.Enqueue(command);
                    }
                });

            return;
        }

    }
}
