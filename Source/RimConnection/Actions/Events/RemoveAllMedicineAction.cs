using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RemoveAllMedicineAction : Action
    {
        public RemoveAllMedicineAction()
        {
            name = "Remove all medicine";
            description = "Hope you don't have any injuries any time soon";
            category = "Event";
            prefix = "Trigger";
            costSilverStore = -1;
            costBitStore = 400;
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;

            var allMedicine = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Medicine).ToList();

            allMedicine.ForEach(medicineThing =>
            {
                Log.Message("Logging one instance of a medicine thing");
                medicineThing.Destroy();
            });

            AlertManager.BadEventNotification("Sorry, but your medicine has been misplaced.... All of it");
        }
    }
}
