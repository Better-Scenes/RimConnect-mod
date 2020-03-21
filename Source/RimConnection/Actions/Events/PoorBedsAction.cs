using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class PoorBedsAction : Action
    {
        public PoorBedsAction()
        {
            name = "Wrong side of the bed";
            description = "Can't sleep, clown'll eat me";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var allBeds = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Bed).ToList();

            allBeds.ForEach(bed =>
            {
                bed.TryGetComp<CompQuality>().SetQuality(QualityCategory.Poor, ArtGenerationContext.Outsider);
            });

            AlertManager.NormalEventNotification("Can't sleep, clown'll eat me. All your beds are now terrible");
        }
    }
}
