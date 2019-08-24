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


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("exampleBoolExplanation", ref settings.exampleBool, "exampleBoolToolTip");
            listingStandard.Label("exampleFloatExplanation");
            settings.exampleFloat = listingStandard.Slider(settings.exampleFloat, 100f, 300f);

            settings.exampleString = listingStandard.TextEntryLabeled("Example String", "something");

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "RimConnection";
        }
    }
}
