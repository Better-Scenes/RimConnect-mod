using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class GrowAllCropsAction : Action
    {
        public GrowAllCropsAction()
        {
            Name = "Grow all crops";
            Description = "The Greek goddess Demeter was just passing by";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;
            var things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Plant);

            // Filter plants, then cast to plant, then check if it's a crop that's not grown
            var crops = things.Where(thing => thing is Plant)
                              .Select(plant => (Plant)plant)
                              .Where(plant => plant.IsCrop && plant.LifeStage == PlantLifeStage.Growing)
                              .ToList();

            crops.ForEach(crop => crop.Growth = 1);

            AlertManager.NormalEventNotification("({0}) The Greek goddess Demeter was just passing by, all your crops have grown!", boughtBy);
        }
    }
}
