using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;

namespace RimConnection
{
    class ServerInitialise
    {
        private RestClient client;

        public ServerInitialise()
        {
            Log.Message("Trying to intialise with server");
            Log.Message($"initialising for {Settings.username}");
            var BASE_URL = Settings.BASE_URL;
            var validCommands = Settings.validCommands;

            client = new RestClient(BASE_URL);
            var request = new RestRequest("command/valid", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("username", Settings.username);
            request.AddJsonBody(new { validCommands });

            //var response = client.Execute<SpawnCommand>(request);
        }
    }
}
