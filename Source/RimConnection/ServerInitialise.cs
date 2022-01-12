using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using RimWorld;

namespace RimConnection
{
    [StaticConstructorOnStartup]
    public static class ServerInitialise
    {
        static ServerInitialise() { Init(); }

        public static bool Init()
        {
            // Generate this before validation so that MP clients have this list available otherwise
            // desyncs occur
            ValidCommandPayloadGenerator validCommandPayloadGenerator = ActionList.ActionListToApi();
            try
            {
                Log.Message("Initialising Server");

                var authed = RimConnectAPI.AuthSecret(RimConnectSettings.secret, out string Token);
                if (!authed)
                {
                    Log.Error("Unable to Connect to RimConnect server.");
                    RimConnectSettings.token = "";
                    return false;
                }

                RimConnectSettings.token = Token;
                RimConnectAPI.PostValidCommands(validCommandPayloadGenerator);
                RimConnectAPI.GetConfig();

                //string worldName = Find.World.info.name;
                var world = Find.World;
                if(world != null)
                {
                    Log.Message($"World name is {world.info.name}");
                    RimConnectAPI.UpdateWorld(world.info.name);
                }

                return true;
            } catch (Exception err)
            {
                Log.Error(err.ToString());

                return false;
            }
        }
    }
}
