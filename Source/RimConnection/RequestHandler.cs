using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Verse;
using System.IO;

using RestSharp;

namespace RimConnection
{

    class RequestHandler
    {
        private string BASE_URL;
        private RestClient client;
        private RestRequest baseRequest;

        public RequestHandler()
        {
            BASE_URL = Settings.BASE_URL;
            client = new RestClient(BASE_URL);
            baseRequest = new RestRequest("spawn");
            baseRequest.AddHeader("Content-Type", "application/json");
            baseRequest.AddHeader("username", Settings.username);
            Log.Message("RequestHandler initialized");
        }

        public string getCommands()
        {
            Log.Message("Requesting object from " + BASE_URL);
            var response = client.Execute<SpawnCommand>(baseRequest);
            Log.Message("Message response:");
            Log.Message(response.Data.command);
            // do we convert to object here?
            // create a class/typedef for Commands and create a new one from the string
            return response.Data.command;
        }
    }
}
