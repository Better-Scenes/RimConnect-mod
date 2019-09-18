using System.Collections.Generic;
using Verse;
using RestSharp;

namespace RimConnection
{

    public static class RequestHandler
    {
        private static string BASE_URL;
        private static RestClient client;
        private static RestRequest baseRequest;

        static RequestHandler()
        {
            BASE_URL = Settings.BASE_URL;
            client = new RestClient(BASE_URL);
            baseRequest = new RestRequest("command/list");
            baseRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {Settings.token}");
        }

        public static List<Command> GetCommands()
        {
            
            Log.Message("Requesting object from " + BASE_URL);
            try
            {
                var response = client.Execute<CommandList>(baseRequest);
                return response.Data.commands;
            } catch
            {
                throw;
            }

        }
    }
}
