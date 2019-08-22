using System;
using System.Timers;
using Verse;

namespace RimConnection
{
    class ServerRequester
    {
        private static System.Timers.Timer timer;
        private static RequestHandler requester = new RequestHandler();

        public ServerRequester(int intervalMs)
        {
            timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += onTimerTimeout;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void onTimerTimeout(Object source, ElapsedEventArgs e)
        {
            Log.Message("Time timed out");

            string res = requester.getCommands();
            // should return a Command object and not a string
            Log.Message(res);
            //DropPodManager should accept a Command object and spawn the specified 
            DropPodManager.createDrop();
        }

    }
}
