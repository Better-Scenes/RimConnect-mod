using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using Verse;

namespace RimConnection
{
    class HttpThing
    {
        public static void SimpleListenerExample(string[] prefixes)
        {
            try
            {
                if (!HttpListener.IsSupported)
                {
                    Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                    return;
                }

                if (prefixes == null || prefixes.Length == 0)
                    throw new ArgumentException("prefixes");

                // Create a listener.
                HttpListener listener = new HttpListener();
                // Add the prefixes.
                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }
                listener.Start();
                Log.Message("Listening...");

                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            } catch (Exception e)
            {
                Log.Message(e.ToString());
            }
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            try
            {
                HttpListener listener = (HttpListener)result.AsyncState;
                // Call EndGetContext to complete the asynchronous operation.
                Log.Message("Hey we got a request");
                HttpListenerContext context = listener.EndGetContext(result);


                HttpListenerRequest request = context.Request;
                var a = request.QueryString;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "{successfull: true}";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                DropPodManager.createDrop();
                // Create the listener again
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            } catch (Exception e) {
                Log.Message(e.ToString());
            }
        }
    }
}
