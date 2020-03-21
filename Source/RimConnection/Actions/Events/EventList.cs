using System.Collections.Generic;

namespace RimConnection
{
    public static class EventList {
        public static List<IAction> eventList = new List<IAction>
        {
            new AllRangedWeaponsToBowsAction(),
            new AnimalMetamorphosisAction(),
            new AnimalSelfTameAction(),
            new BeaversAction(),
            new BionicPlagueAction(),
            new CargoPodAction(),
            new CleanAllFilthAction(),
            new ClearAddictionsAction(),
            new FarmAnimalsWanderInAction(),
            new GoldenShowerAction(),
            new GrowAllCropsAction(),
            new HealingNanitesAction(),
            new HeartAttackRandomColonistAction(),
            new LuciferiumAddictionAction(),
            new ManhunterPackAction(),
            new PsychicDroneAction(),
            new PsychicSootheAction(),
            new RaidAction(),
            new RaidDropAction(),
            new RaidSiegeAction(),
            new RainingMenAction(),
            new RatSwarmEvent(),
            new RemoveAllMedicineAction()
        };
    }
}
