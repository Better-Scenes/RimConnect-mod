using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class RandomWeaponAction : Action
    {
        public RandomWeaponAction()
        {
            this.name = "Random Weapon";
            this.description = "The best defence is a good offence";
            this.canSpawnMultiple = true;
            this.category = "Gear";
        }

        public override void execute(int amount)
        {
            var weaponThingDefs = DefDatabase<ThingDef>.AllDefs.Where(def => { return def.equipmentType == EquipmentType.Primary; });
            
            DropPodManager.createDrop(weaponThingDefs.RandomElement(), amount, name, description);
        }
    }
}
