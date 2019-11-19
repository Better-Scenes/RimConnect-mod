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
            name = "Manhunter Pack";
            description = "They hunger for man-flesh";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;
            var manhunterWorker = new IncidentWorker_ManhunterPack();
            manhunterWorker.def = IncidentDef.Named("ManhunterPack");

            manhunterWorker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Manhunter Pack!");

        }
    }
}
