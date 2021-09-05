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
                AlertManager.NormalEventNotification("Your viewers tried to tame some animals, but there were none around!");
            }

            var animalsToTame = animalCandidates.InRandomOrder().Take(amount).ToList();

            animalsToTame.ForEach(animal =>
            {
                animal.SetFaction(Faction.OfPlayer);
            });

            if (boughtBy == "Poll") { boughtBy = "Your twitch viewers"; }
            string notificationMessage = $"<color=#9147ff>{boughtBy}</color> has done some taming for you!";

            AlertManager.NormalEventNotification(notificationMessage);
        }
    }
}
