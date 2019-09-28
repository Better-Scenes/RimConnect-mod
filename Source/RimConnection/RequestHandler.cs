using System.Collections.Generic;
using Verse;
using RestSharp;

namespace RimConnection
{

    public static class RequestHandler
    {
        private static string BASE_URL;
        private static RestClient client;

        static RequestHandler()
        {
            BASE_URL = Settings.BASE_URL;
            client = new RestClient(BASE_URL);
        }

        public static List<Command> GetCommands()
        {
            var baseRequest = new RestRequest("command/list");
            baseRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {Settings.token}");

            try
            {
                var response = client.Execute<CommandList>(baseRequest);
                var commands = response.Data.commands;

                DeleteCommands(commands.Count);
                return commands;
            } catch
            {
                throw;
            }

        }

        private static void DeleteCommands(int number)
        {
            if (number <= 0)
            {
                return;
            }
            try
            {

                var baseRequest = new RestRequest("command/list", Method.DELETE);
                baseRequest.AddHeader("Content-Type", "application/json")
                           .AddHeader("Authorization", $"Bearer {Settings.token}")
                           .AddParameter("toDelete", number);

                var response = client.Execute(baseRequest);
            } catch
            {
                throw;
            }
        }
    }
}
