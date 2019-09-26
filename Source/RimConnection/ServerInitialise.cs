using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using RimWorld;

namespace RimConnection
{
    public static class Initialise
    {
        static Initialise() { }

        public static void init()
        {
            Log.Message("Trying to intialise with server");
            var BASE_URL = Settings.BASE_URL;

            RestClient client = new RestClient(BASE_URL);

            // Get a new JWT from the server based on the secret
            var authModRequest = new RestRequest("auth/mod", Method.POST);
            authModRequest.AddHeader("Content-Type", "application/json")
                          .AddJsonBody(new AuthMod());

            var authModResponse = client.Execute<AuthModResponse>(authModRequest);
            Settings.token = authModResponse.Data.token;
            Log.Message(Settings.token);

            // Go and push all the valid commands to the server
            var validCommandRequest = new RestRequest("command/valid", Method.POST);
            validCommandRequest.AddHeader("Content-Type", "application/json")
                   .AddHeader("Authorization", $"Bearer {Settings.token}")
                   .AddJsonBody(ActionList.ActionListToApi());

            var validCommandResponse = client.Execute<ValidCommand>(validCommandRequest);
        }
    }
}
