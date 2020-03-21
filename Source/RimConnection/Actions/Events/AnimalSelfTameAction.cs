using RimWorld;
using System.Linq;
using Verse;

namespace RimConnection
{
    class AnimalSelfTameAction : Action
    {
        public AnimalSelfTameAction()
        {
            name = "Animal Self Tame";
            description = "You don't get to choose, hope it was a good one";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = 0;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var animalCandidates = currentMap.mapPawns.AllPawnsSpawned.Where(delegate (Pawn x)
            {
                return x.RaceProps.Animal && x.Faction == null && !x.Position.Fogged(x.Map) && !x.InMentalState && !x.Downed;
            });

            if(animalCandidates.Count() < 1)
            {
                AlertManager.NormalEventNotification("Your viewers tried to tame some animals, but there were none around!");
            }

            var animalsToTame = animalCandidates.InRandomOrder().Take(amount).ToList();

            animalsToTame.ForEach(animal =>
            {
                animal.SetFaction(Faction.OfPlayer);
            });

            AlertManager.NormalEventNotification("Your viewers have done some taming for you!");
        }
    }
}
