using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class GrowAllCropsAction : Action
    {
        public GrowAllCropsAction()
        {
            name = "Grow all crops";
            description = "The greek god Demeter was just passing by";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            var things = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Plant);

            // Filter plants, then cast to plant, then check if it's a crop that's not grown
            var crops = things.Where(thing => thing is Plant)
                              .Select(plant => (Plant)plant)
                              .Where(plant => plant.IsCrop && plant.LifeStage == PlantLifeStage.Growing)
                              .ToList();

            crops.ForEach(crop => crop.Growth = 1);

            Find.LetterStack.ReceiveLetter("Twitch Event", "The greek god Demeter was just passing by, all your grops have grown!", LetterDefOf.PositiveEvent);
        }
    }
}
