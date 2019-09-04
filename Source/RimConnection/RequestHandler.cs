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
            baseRequest = new RestRequest("command/list");
            baseRequest.AddHeader("Content-Type", "application/json");
            baseRequest.AddHeader("username", Settings.username);
            Log.Message("RequestHandler initialized");
        }

        public List<Command> getCommands()
        {
            Log.Message("Requesting object from " + BASE_URL);
            var response = client.Execute<CommandList>(baseRequest);
            return response.Data.commands;
        }
    }
}
