using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using RimWorld;
using RimConnection.Windows;

namespace RimConnection
{
    [StaticConstructorOnStartup]
    public static class ServerInitialise
    {
        static ServerInitialise() { Init(); }

        public static bool Init()
        {
            try
            {
                Log.Message("Initialising Server");
                RimConnectSettings.token = RimConnectAPI.AuthSecret(RimConnectSettings.secret);
                RimConnectAPI.PostValidCommands(ActionList.ActionListToApi());

                return true;
            } catch (Exception err)
            {
                Log.Error(err.ToString());

                BugReport.CreateBugReport("Unable to Connect to RimConnect server.");


                return false;
            }
        }
    }
}
