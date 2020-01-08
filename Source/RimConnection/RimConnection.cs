using UnityEngine;
using Verse;

namespace RimConnection
{
    public class RimConnection: Mod
    {
        RimConnectSettings settings;


        public RimConnection(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<RimConnectSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            this.settings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "RimConnection";
        }
    }
}
