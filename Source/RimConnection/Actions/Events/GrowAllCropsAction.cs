using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class GrowAllCropsAction : Action
    {
        public GrowAllCropsAction()
        {
            this.name = "Grow all crops";
            this.description = "The greek god Demeter was just passing by";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            var things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Plant);

            // Filter plants, then cast to plant, then check if it's a crop that's not grown
            var crops = things.Where(thing => thing is Plant)
                              .Select(plant => (Plant)plant)
                              .Where(plant => plant.IsCrop && plant.LifeStage == PlantLifeStage.Growing)
                              .ToList();

            crops.ForEach(crop => crop.Growth = 1);

            AlertManager.NormalEventNotification("The greek god Demeter was just passing by, all your crops have grown!");
        }
    }
}
