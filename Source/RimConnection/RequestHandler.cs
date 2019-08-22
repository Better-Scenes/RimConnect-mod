using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Verse;
using System.IO;

namespace RimConnection
{

    class RequestHandler
    {
        private const string HOST_URL = "http://localhost:8080/spawn/";
        private static WebClient client = new WebClient();

        public RequestHandler()
        {
            Log.Message("RequestHandler initialized");
        }

        public string getCommands()
        {
            Log.Message("Opening stream at " + HOST_URL);
            Stream stream = client.OpenRead(HOST_URL);
            Log.Message("Stream open, reading now");
            StreamReader sr = new StreamReader(stream);
            String response = sr.ReadToEnd();
            Log.Message("Got response..." + response);
            stream.Close();
            Log.Message("Closed Stream");
            // do we convert to object here?
            // create a class/typedef for Commands and create a new one from the string
            return response;
        }
    }
}
