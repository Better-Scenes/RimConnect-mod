using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Verse;

namespace RimConnection
{
    class ServerPoller : GameComponent
    {
        private float timeCounterSeconds = 0.0f;
        private float waitTimeSeconds = 30.0f;
        static Queue<Command> commandQueue = new Queue<Command>();

        private DateTime previousDateTime;

        public ServerPoller(Game game)
        {
        }

        public ServerPoller()
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            previousDateTime = DateTime.Now;
        }

        public override void GameComponentTick()
        {
            // Only do this stuff if the mod successfully connected to the server
            if (RimConnectSettings.initialiseSuccessful)
            {
                base.GameComponentTick();
                var now = DateTime.Now;

                timeCounterSeconds += (float)(now - previousDateTime).TotalSeconds;
                if (timeCounterSeconds > waitTimeSeconds)
                {
                    timeCounterSeconds = 0.0f;
                    serverChecker();
                }

                previousDateTime = now;
            }

            if (commandQueue.Count > 0)
            {
                Command command = commandQueue.Dequeue();

                IAction action = ActionList.actionLookup[command.actionHash];
                action.Execute(command.amount);
                Find.TickManager.slower.SignalForceNormalSpeedShort();
            }
        }

        public void serverChecker()
        {
            var commands = RimConnectAPI.GetCommands();
            foreach (var command in commands)
            {
                commandQueue.Enqueue(command);
            }
            return;
        }

    }
}
