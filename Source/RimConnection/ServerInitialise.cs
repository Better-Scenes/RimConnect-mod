using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using RimWorld;

namespace RimConnection
{
    public static class ServerInitialise
    {
        static ServerInitialise() { }

        public static bool init()
        {
            var BASE_URL = Settings.BASE_URL;

            RestClient client = new RestClient(BASE_URL);

            // Get a new JWT from the server based on the secret
            var authModRequest = new RestRequest("auth/mod", Method.POST);
            authModRequest.AddHeader("Content-Type", "application/json")
                          .AddJsonBody(new AuthMod());

            var authModResponse = client.Execute<AuthModResponse>(authModRequest);

            // If the request failed, return early and post a message
            if(authModResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Settings.initialiseSuccessful = false;
                Log.Message("Failed to connect. Is your secret correct?");
                return false;
            } else
            {
                Log.Message("Successfully authenticated with server!");
            }

            Settings.token = authModResponse.Data.token;

            // Go and push all the valid commands to the server
            var validCommandRequest = new RestRequest("command/valid", Method.POST);
            validCommandRequest.AddHeader("Content-Type", "application/json")
                   .AddHeader("Authorization", $"Bearer {Settings.token}")
                   .AddJsonBody(ActionList.ActionListToApi());

            var validCommandResponse = client.Execute<ValidCommand>(validCommandRequest);

            if(validCommandResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Settings.initialiseSuccessful = false;
                Log.Message("Failed to provide valid commands to server");
                return false;
            }
            Settings.initialiseSuccessful = true;
            return true;
        }
    }
}
