using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using UnityEngine;
using Verse;



namespace RimConnection
{
    public class RimConnection: Mod
    {
        Settings settings;

        public RimConnection(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Settings>();
            Log.Message("Hello World!");

            new ServerRequester(5000);
        }


        public override string SettingsCategory()
        {
            return "RimConnection";
        }
    }
}
