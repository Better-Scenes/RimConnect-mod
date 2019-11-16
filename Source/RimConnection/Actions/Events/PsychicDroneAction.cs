using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class PsychicDroneAction : Action
    {
        public PsychicDroneAction()
        {
            this.name = "Psychic Drone";
            this.description = "Can you hear that? Man that's annoying";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, currentMap);
            parms.forced = true;

            var worker = new IncidentWorker_PsychicDrone();
            worker.def = IncidentDef.Named("PsychicDrone");

            worker.TryExecute(parms);
            AlertManager.BadEventNotification("Your viewers have sent a Phsychic Drone");
        }
    }
}
