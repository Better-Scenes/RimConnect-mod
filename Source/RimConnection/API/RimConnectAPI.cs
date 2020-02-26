using System.Collections.Generic;
using Verse;
using RestSharp;

namespace RimConnection
{

    public static class RimConnectAPI
    {
        private static string BASE_URL;
        private static RestClient client;

        static RimConnectAPI()
        {
            BASE_URL = Settings.BASE_URL;
            client = new RestClient(BASE_URL);
        }

        public static string AuthSecret(string secret)
        {
            // Get a new JWT from the server based on the secret
            var authModRequest = new RestRequest("auth/mod", Method.POST);
            authModRequest.AddHeader("Content-Type", "application/json")
                          .AddJsonBody(new AuthMod());

            var authModResponse = client.Execute<AuthModResponse>(authModRequest);

            // If the request failed, throw and post a message
            if (authModResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Settings.initialiseSuccessful = false;
                if(BASE_URL.Contains("localhost"))
                {
                    throw new System.Exception("The developer is an idiot, and you need to tell him that he left localhost in the settings");
                }
                throw new System.Exception("Failed to connect. Is your secret correct?");
            }

            Log.Message("Successfully authenticated with server!");
            return authModResponse.Data.token;
        }

        public static void PostValidCommands(ValidCommandList commandList)
        {
            // Go and push all the valid commands to the server
            var validCommandRequest = new RestRequest("command/valid", Method.POST);
            validCommandRequest.AddHeader("Content-Type", "application/json")
                   .AddHeader("Authorization", $"Bearer {Settings.token}")
                   .AddJsonBody(commandList);

            var validCommandResponse = client.Execute<ValidCommand>(validCommandRequest);

            if (validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Settings.initialiseSuccessful = false;
                Log.Error("Failed to provide valid commands to server");
                throw new System.Exception("Failed to provide valid commands to server");
            }
            Settings.initialiseSuccessful = true;
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
                           .AddQueryParameter("toDelete", number.ToString(), false);

                var response = client.Execute(baseRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
