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

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var allBeds = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Bed);
            foreach (var thing in allBeds)
            {
                var compQuality = thing.TryGetComp<CompQuality>();
                if (compQuality != null)
                {
                    compQuality.SetQuality(QualityCategory.Masterwork, ArtGenerationContext.Outsider);
                }
            }

            AlertManager.NormalEventNotification("({0}) I feel like a toasty cinnamon bun, I never want to leave this bed. All your beds are masterwork!", boughtBy);
        }

    }
}
