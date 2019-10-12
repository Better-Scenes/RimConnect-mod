using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AllRangedWeaponsToBowsAction : Action
    {
        public AllRangedWeaponsToBowsAction()
        {
            this.name = "Ranged weapons to bows";
            this.description = ".....?!";
            this.category = "Event";
        }

        public override void execute(int amount)
        {
            var allWeapons = new ListerThings(ListerThingsUse.Region).ThingsInGroup(ThingRequestGroup.Weapon);
            foreach (var weapon in allWeapons)
            {
                
            }
        }
    }
}
