using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class ManhunterPackAction : Action
    {
        public ManhunterPackAction()
        {
            this.name = "Manhunter Pack";
            this.description = "They hunger for man-flesh";
            this.category = "Event";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var manhunterWorker = new IncidentWorker_ManhunterPack();
            manhunterWorker.def = IncidentDef.Named("ManhunterPack");

            manhunterWorker.TryExecute(parms);
        }
    }
}
