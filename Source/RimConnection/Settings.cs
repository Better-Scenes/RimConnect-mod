using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    public class Settings : ModSettings
    {
        public bool exampleBool;
        public string exampleString;
        public float exampleFloat;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref exampleBool, "exampleBool");
            Scribe_Values.Look(ref exampleString, "exampleString");
            Scribe_Values.Look(ref exampleFloat, "exampleFloat");
            base.ExposeData();
        }
    }
}
