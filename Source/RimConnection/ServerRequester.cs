using System;
using System.Timers;

namespace RimConnection
{
    class ServerRequester
    {
        private static System.Timers.Timer timer;

        public ServerRequester(int intervalMs)
        {
            timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += onTimerTimeout;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void onTimerTimeout(Object source, ElapsedEventArgs e)
        {
            DropPodManager.createDrop();
        }

    }
}
