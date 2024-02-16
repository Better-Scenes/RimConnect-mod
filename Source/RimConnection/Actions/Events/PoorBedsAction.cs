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
            Name = "Wrong side of the bed";
            Description = "Can't sleep, clown'll eat me";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var allBeds = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Bed).ToList();

            allBeds.ForEach(bed =>
            {
                bed.TryGetComp<CompQuality>().SetQuality(QualityCategory.Poor, ArtGenerationContext.Outsider);
            });

            AlertManager.NormalEventNotification("({0}) Can't sleep, clown'll eat me. All your beds are now terrible.", boughtBy);
        }
    }
}
