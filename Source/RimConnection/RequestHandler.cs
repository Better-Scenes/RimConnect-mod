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
        private const string BASE_URL = "http://localhost:8080/";

        private RestClient client;
        private RestRequest baseRequest;
        public RequestHandler()
        {
            client = new RestClient(BASE_URL);
            baseRequest = new RestRequest("spawn");
            Log.Message("RequestHandler initialized");
        }

        public string getCommands()
        {
            Log.Message("Requesting object from " + BASE_URL);
            IRestResponse response = client.Execute(baseRequest);
            Log.Message("Message response:");
            Log.Message(response.Content);
            // do we convert to object here?
            // create a class/typedef for Commands and create a new one from the string
            return response.Content;
        }
    }
}
