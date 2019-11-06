using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class EventList {
        public static List<Action> eventList = new List<Action>
        {
            new EventCategoryAction(),
            new AllRangedWeaponsToBowsAction(),
            new AnimalSelfTameAction(),
            new BeaversAction(),
            new CargoPodAction(),
            new FarmAnimalsWanderInAction(),
            new GrowAllCropsAction(),
            new KillRandomColonistAction(),
            new ManhunterPackAction(),
            new PsychicDroneAction(),
            new PsychicSootheAction(),
            new RaidAction(),
            new RaidDropAction(),
            new RaidSiegeAction(),
            new RemoveAllMedicineAction()
        };
    }
}
