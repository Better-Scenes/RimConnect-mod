using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class PsychicSootheAction : Action
    {
        public PsychicSootheAction()
        {
            Name = "Psychic Soothe";
            Description = "Why doesn't everyone just like, chill out";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var worker = new IncidentWorker_PsychicSoothe();
            worker.def = IncidentDef.Named("PsychicSoothe");

            worker.TryExecute(parms);
            AlertManager.NormalEventNotification("{0} sent a Psychic Soothe!", boughtBy);
        }
    }
}
