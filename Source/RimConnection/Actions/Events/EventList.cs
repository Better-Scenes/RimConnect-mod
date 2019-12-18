﻿using System.Collections.Generic;

namespace RimConnection
{
    public static class EventList {
        public static List<IAction> eventList = new List<IAction>
        {
            new AllRangedWeaponsToBowsAction(),
            new AnimalSelfTameAction(),
            new BeaversAction(),
            new BionicPlagueAction(),
            new CargoPodAction(),
            new FarmAnimalsWanderInAction(),
            new GoldenShowerAction(),
            new GrowAllCropsAction(),
            new HealingNanitesAction(),
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