using System.Collections.Generic;
using Verse;
using RestSharp;
using System;

namespace RimConnection
{

    public static class RimConnectAPI
    {
        private static string BASE_URL;
        private static RestClient client;

        static RimConnectAPI()
        {
            BASE_URL = RimConnectSettings.BASE_URL;
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
                RimConnectSettings.initialiseSuccessful = false;
                if(BASE_URL.Contains("localhost"))
                {
                    throw new System.Exception("The developer is an idiot, and you need to tell him that he left localhost in the settings");
                }
                Log.Warning("Failed to connect. Is your secret correct?");
            }
            else
            {
                Log.Message("Successfully authenticated with server!");
            }

            return authModResponse.Data.token;
        }

        public static void PostValidCommands(ValidCommandList commandList)
        {
            try
            {
                // Go and push all the valid commands to the server
                var validCommandRequest = new RestRequest("command/valid", Method.POST);
                validCommandRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                       .AddJsonBody(commandList);

                var validCommandResponse = client.Execute<CommandOptionList>(validCommandRequest);

                if (validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    RimConnectSettings.initialiseSuccessful = false;
                    Log.Warning("Failed to provide valid commands to server");
                }
                else
                {
                    RimConnectSettings.initialiseSuccessful = true;

                    CommandOptionList commandOptionList = new CommandOptionList();
                    commandOptionList.commandOptions = validCommandResponse.Data.commandOptions;

                    Settings.CommandOptionListController.commandOptionList = commandOptionList;
                    Log.Message("Provided valid commands to the server");
                }
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
            }
        }

        public static void PostUpdatedCommandOptions(CommandOptionList updatedCommandOptionList)
        {
            // Send Updated CommandOption Settings
            try
            {
                // Go and push all the valid commands to the server
                var commandOptionRequest = new RestRequest("command/options", Method.POST);
                commandOptionRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                       .AddJsonBody(updatedCommandOptionList);

                var validCommandResponse = client.Execute<CommandOptionList>(commandOptionRequest);

                if (validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Log.Warning("Failed to update command options to server");
                }
                else
                {
                    Log.Message("Updated command options to server");
                }
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
            }
        }

        public static List<Command> GetCommands()
        {
            var baseRequest = new RestRequest("command/list");
            baseRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}");

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

        // This needs to be updated to specify what command should be deleted instead of a number
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
                           .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                           .AddParameter("toDelete", number);

                var response = client.Execute(baseRequest);
            } catch
            {
                throw;
            }
        }

        public static void UpdateWorld(string world)
        {
            var baseRequest = new RestRequest("mod/world", Method.POST);
            baseRequest.AddHeader("Content-Type", "application/json")
                           .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                           .AddJsonBody(new PostWorld { world = world});
            try
            {
                var response = client.Execute(baseRequest);
            } catch
            {
                throw;
            }
        }
    }
}
