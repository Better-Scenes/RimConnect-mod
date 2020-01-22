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
            try
            {
                RimConnectSettings.token = RimConnectAPI.AuthSecret(RimConnectSettings.secret);
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
