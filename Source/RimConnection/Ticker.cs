using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Verse;

namespace RimConnection
{
    public class Ticker : Thing
    {
        static Game game;
        static Thread thread;
        static Ticker _instance;

        public Ticker()
        {
            def = new ThingDef { tickerType = TickerType.Normal, isSaveable = false };
            thread = new Thread(Register);
            thread.Start();
        }


        public static Ticker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Ticker();
                }

                return _instance;
            }
        }

        void Register()
        {
            while (true)
            {
                try
                {
                    if (game != Current.Game)
                    {
                        if (game != null)
                        {
                            game.tickManager.DeRegisterAllTickabilityFor(this);
                            game = null;
                        }

                        game = Current.Game;
                        if (game != null)
                        {
                            game = Current.Game;
                            game.tickManager.RegisterAllTickabilityFor(this);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning("Exception: " + ex.Message + "\n" + ex.StackTrace);
                }
                finally
                {
                    Thread.Sleep(30000);
                }
            }    
        }

        public override void Tick()
        {
            ServerPoller serverPoller = Current.Game.GetComponent<ServerPoller>();
            serverPoller.serverChecker();

            Messages.Message(new Message("Polling Server for Commands", MessageTypeDefOf.NeutralEvent));
        }
    }
}
