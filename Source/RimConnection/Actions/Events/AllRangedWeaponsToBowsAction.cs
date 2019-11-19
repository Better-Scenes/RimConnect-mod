using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AllRangedWeaponsToBowsAction : Action
    {
        public AllRangedWeaponsToBowsAction()
        {
            name = "Ranged weapons to bows";
            description = ".....?!";
            category = "Event";
            prefix = "Trigger";
        }

        public override void Execute(int amount)
        {
            var currentMap = Find.CurrentMap;
            var weapons = currentMap.listerThings.ThingsInGroup(ThingRequestGroup.Weapon).ToList();

            // find and replace all the ranged weapons on the ground
            weapons.ForEach(weapon =>
            {
                if (weapon.def.IsRangedWeapon)
                {
                    var weaponComp = weapon.TryGetComp<CompQuality>();
                    if(weaponComp == null)
                    {
                        return;
                    }
                    var weaponQuality = weaponComp.Quality;

                    Log.Message(weaponQuality.ToString());
                    var oldWepPosition = weapon.Position;
                    weapon.Destroy();
                    
                    var newBow = ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Bow_Short"));
                    var newBowComp = newBow.TryGetComp<CompQuality>();

                    newBowComp.SetQuality(weaponQuality, ArtGenerationContext.Colony);

                    GenSpawn.Spawn(newBow, oldWepPosition, currentMap);
                }
            });

            // find and replace all the ranged weapons in colonist's inventories
            var allColonists = Find.ColonistBar.GetColonistsInOrder().ToList();
            allColonists.ForEach(colonist =>
            {
                var colonistPrimary = colonist.equipment.Primary;

                if(colonistPrimary != null && colonistPrimary.def.IsRangedWeapon)
                {
                    var weaponComp = colonistPrimary.TryGetComp<CompQuality>();
                    if(weaponComp == null)
                    {
                        return;
                    }

                    var weaponQuality = weaponComp.Quality;
                    colonist.equipment.Remove(colonistPrimary);
                    var newBow = ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Bow_Short"));
                    var newBowComp = newBow.TryGetComp<CompQuality>();
                    newBowComp.SetQuality(weaponQuality, ArtGenerationContext.Colony);

                    colonist.equipment.AddEquipment((ThingWithComps)newBow);
                }
            });
            AlertManager.NormalEventNotification("I hope you like bows, because all your ranged weapons are now bows");
        }
    }
}
