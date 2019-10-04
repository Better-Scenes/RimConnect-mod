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
            this.name = "Psychic Soothe";
            this.description = "Why doesn't everyone just like, chill out";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            new IncidentWorker_PsychicSoothe().TryExecute(parms);
        }
    }
}
