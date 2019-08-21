using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;



namespace RimConnection
{
    public class BaseClass: Mod
    {
        Settings settings;

        public BaseClass(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Settings>();
            Log.Message("Hello World!");

            string[] prefixes = { "http://localhost:8080/" };
            HttpThing.SimpleListenerExample(prefixes);
            new ServerRequester(5000);
        }
    }
}
