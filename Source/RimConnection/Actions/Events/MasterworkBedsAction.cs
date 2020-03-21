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
            name = "Good night's sleep";
            description = "I feel like a toasty cinnamon bun, I never want to leave this bed";
            category = "Event";
            prefix = "Trigger";
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
