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
            ValidCommandList commandList = ActionList.ActionListToApi();
            try
            {
                Log.Message("Initialising Server");

                var authed = RimConnectAPI.AuthSecret(RimConnectSettings.secret, out string Token);
                if (!authed)
                {
                    Log.Error("Unable to Connect to RimConnect server.");
                    return false;
                }

                RimConnectSettings.token = Token;
                RimConnectAPI.PostValidCommands(commandList);
                RimConnectAPI.GetConfig();

                return true;
            } catch (Exception err)
            {
                Log.Error(err.ToString());

                return false;
            }
        }
    }
}
