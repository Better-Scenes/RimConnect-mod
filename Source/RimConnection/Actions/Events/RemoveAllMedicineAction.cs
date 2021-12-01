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
            Name = "Remove all medicine";
            Description = "Hope you don't have any injuries any time soon";
            Category = "Event";
            Prefix = "Trigger";
            CostSilverStore = 0;
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var allMedicine = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Medicine).ToList();

            allMedicine.ForEach(medicineThing =>
            {
                medicineThing.Destroy();
            });

            AlertManager.BadEventNotification("Sorry, but {0} seems to have misplaced all your medicine!", boughtBy);
        }
    }
}
