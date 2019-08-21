using System;
using System.Timers;

namespace RimConnection
{
    class ServerTimer
    {
        private static System.Timers.Timer timer;
        public ServerTimer(int intervalMs, Func<Timer, string>)
        {
            timer = new System.Timers.Timer(intervalMs);
        }


    }
}
