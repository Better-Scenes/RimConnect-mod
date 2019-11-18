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
            this.name = "Remove all medicine";
            this.description = "Hope you don't have any injuries any time soon";
            this.category = "Event";
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

            Find.LetterStack.ReceiveLetter("Twitch Event", "Sorry, but your medicine has been misplaced.... All of it", LetterDefOf.NegativeEvent);
        }
    }
}
