using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using RimWorld;

namespace RimConnection
{
    public static class ServerInitialise
    {
        static ServerInitialise() { }

        public static bool Init()
        {
            try
            {
                Settings.token = RimConnectAPI.AuthSecret(Settings.secret);
                RimConnectAPI.PostValidCommands(ActionList.ActionListToApi());
                return true;
            } catch (Exception err)
            {
                Log.Error(err.ToString());
                return false;
            }
        }
    }
}
