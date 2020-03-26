using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class MasterworkBedsAction : Action
    {
        public MasterworkBedsAction()
        {
            Name = "Good night's sleep";
            Description = "I feel like a toasty cinnamon bun, I never want to leave this bed";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var allBeds = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Bed).ToList();

            allBeds.ForEach(bed =>
            {
                bed.TryGetComp<CompQuality>().SetQuality(QualityCategory.Masterwork, ArtGenerationContext.Outsider);
            });

            AlertManager.NormalEventNotification("I feel like a toasty cinnamon bun, I never want to leave this bed. All your beds are masterwork!");
        }
    }
}
