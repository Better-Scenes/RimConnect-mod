using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class EventList {
        public static List<IAction> eventList = new List<IAction>
        {
            new EventCategoryAction(),
            new AllRangedWeaponsToBowsAction(),
            new AnimalSelfTameAction(),
            new BeaversAction(),
            new CargoPodAction(),
            new FarmAnimalsWanderInAction(),
            new GoldenShowerAction(),
            new GrowAllCropsAction(),
            new HeartAttackRandomColonistAction(),
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
