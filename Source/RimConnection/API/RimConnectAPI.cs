using System.Collections.Generic;
using Verse;
using RestSharp;
using System;
using RimConnection.API;

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

        public static void ChangeBaseURL(string baseUrl)
        {
            client = new RestClient(baseUrl);
            Log.Warning("RimConnectAPI baseurl changed to " + baseUrl);
        }

        public static bool AuthSecret(string secret, out string response)
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
                    Log.Warning("The developer is an idiot, and you need to tell him that he left localhost in the settings");
                    response = null;
                    return false;
                }

                Log.Warning("Failed to connect. Is your secret correct?");
                if(secret.Length > 5)
                {
                    Log.Warning($"Final 5 characters of secret: {secret.Substring(secret.Length - 5)}");
                }
                Log.Warning($"URL from Settings:  {BASE_URL}");
                Log.Warning($"URL from client: {client.BaseUrl}");
                Log.Warning($"Response URI:  {authModResponse.ResponseUri}");
                Log.Warning($"HTTP status code: {authModResponse.StatusCode}");
                Log.Warning($"Error message: {authModResponse.ErrorMessage}");
                if(authModResponse.ErrorException != null)
                {
                    Log.Warning($"Error exception message: {authModResponse.ErrorException.Message}");
                    Log.Warning(authModResponse.ErrorException.StackTrace);
                }

                response = null;
                return false;
            }

            Log.Message("Successfully authenticated with server!");
            response = authModResponse.Data.token;
            return true;
        }

        public static void PostValidCommands(ValidCommandPayloadGenerator commandPayloadGenerator)
        {
            try
            {
                while(commandPayloadGenerator.isThereAnotherChunk())
                {
                    ValidCommandListPostPayload payloadChunk = commandPayloadGenerator.getNextChunk();
                    foreach (var command in payloadChunk.validCommands)
                    {
                        if (string.IsNullOrEmpty(command.description))
                        {
                            command.description = "description missing";
                            Log.Warning($"Description missing for {command.name}");
                        }
                    }
                    var validCommandRequest = new RestRequest("command/valid", Method.POST);
                    validCommandRequest.AddHeader("Content-Type", "application/json")
                           .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                           .AddJsonBody(payloadChunk);

                    var validCommandResponse = client.Execute<ValidCommandPostResponse>(validCommandRequest);

                    if (validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        RimConnectSettings.initialiseSuccessful = false;
                        string commandDetails = validCommandResponse.Content ?? "Payload chunk is null";
                        Log.Error($"Failed to provide valid commands to server. Response Details: {commandDetails}");
                        return;
                    }
                }


                // Then go and fetch the commands from the server seperately
                var commands = GetValidCommands();

                RimConnectSettings.initialiseSuccessful = true;

                CommandOptionList commandOptionList = new CommandOptionList();
                commandOptionList.commandOptions = GetValidCommands();

                Settings.CommandOptionListController.commandOptionList = commandOptionList;
                Log.Message($"Provided a total of {commandOptionList.commandOptions.Count} valid commands to the server");
            }
            catch(Exception e)
            {
                Log.Error($"Failed to provide valid commands to server. {e.Message}");
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
                var validCommandResponse = client.Execute(commandOptionRequest);
                if (validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Log.Error($"Failed to update command options to server. {validCommandResponse.StatusCode}");
                }
                else
                {
                    Log.Message("Updated command options to server");
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to update command options to server. {e.Message}");
            }
        }

        public static  List<CommandOption> GetValidCommands()
        {
            var baseRequest = new RestRequest("command/valid");
            baseRequest.AddHeader("Content-Type", "application/json")
                       .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}");

            try
            {
                var response = client.Execute<ValidCommandGetResponse>(baseRequest);
                if (response == null) throw new NullReferenceException("Response is null");
                var validCommands = response.Data.commands;

                return validCommands;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to get commands from server. {e.Message}");
                throw;
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
                if (response == null) throw new NullReferenceException("Response is null");
                var commands = response.Data.commands;
               

                DeleteCommands(commands.Count);
                return commands;
            } catch(Exception e)
            {
                Log.Error($"Failed to get commands from server. {e.Message}");
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
                           .AddQueryParameter("toDelete", number.ToString(), false);

                var response = client.Execute(baseRequest);
            } catch (Exception e)
            {
                Log.Error($"Failed to delete commands from server. {e.Message}");
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
            } catch(Exception e)
            {
                Log.Error($"Failed to provide world info to server. {e.Message}");
                throw;
            }
        }

        public static void GetConfig()
        {
            RestRequest restRequest = new RestRequest("loyalty/config", Method.GET);
            restRequest.AddHeader("Content-Type", "application/json")
                .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}");

            try
            {
                var response = client.Execute<Config>(restRequest);

                if (response == null) throw new NullReferenceException("Response is null");

                RimConnectSettings.silverAwardPoints = response.Data.silverAwardPoints;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public static void PostConfig()
        {
            RestRequest restRequest = new RestRequest("loyalty/config", Method.POST);
                restRequest.AddHeader("Content-Type", "application/json")
                           .AddHeader("Authorization", $"Bearer {RimConnectSettings.token}")
                           .AddJsonBody(new Config { silverAwardPoints = RimConnectSettings.silverAwardPoints });

            try
            {
                var response = client.Execute(restRequest);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to provide Loyalty Config to server. {e.Message}");
                throw;
            }
        }
    }
}
