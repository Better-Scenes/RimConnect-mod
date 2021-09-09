using RimWorld;
using System.Linq;
using Verse;

namespace RimConnection
{
    class AnimalSelfTameAction : Action
    {
        public AnimalSelfTameAction()
        {
            Name = "Animal Self Tame";
            Description = "You don't get to choose, hope it was a good one";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var animalCandidates = currentMap.mapPawns.AllPawnsSpawned.Where(delegate (Pawn x)
            {
                return x.RaceProps.Animal && x.Faction == null && !x.Position.Fogged(x.Map) && !x.InMentalState && !x.Downed;
            });

            if(animalCandidates.Count() < 1)
            {
                AlertManager.NormalEventNotification("{0} tried to tame some animals, but there were none around!", boughtBy);
            }

            var animalsToTame = animalCandidates.InRandomOrder().Take(amount).ToList();

            animalsToTame.ForEach(animal =>
            {
                animal.SetFaction(Faction.OfPlayer);
            });

            AlertManager.NormalEventNotification("{0} did some taming for you!", boughtBy);
        }
    }
}
