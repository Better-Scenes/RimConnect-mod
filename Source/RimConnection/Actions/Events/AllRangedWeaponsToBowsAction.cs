using System.Collections.Generic;
using System.Linq;

using RimWorld;
using Verse;

namespace RimConnection
{
    class AllRangedWeaponsToBowsAction : Action
    {
        public AllRangedWeaponsToBowsAction()
        {
            Name = "Ranged weapons to bows";
            Description = "All ranged weapons turn into bows";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
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

                    var oldWepPosition = weapon.Position;
                    weapon.Destroy();
                    
                    var newBow = ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Bow_Short"));
                    var newBowComp = newBow.TryGetComp<CompQuality>();

                    newBowComp.SetQuality(weaponQuality, ArtGenerationContext.Colony);

                    GenSpawn.Spawn(newBow, oldWepPosition, currentMap);
                }
            });

            // find and replace all the ranged weapons in colonist's inventories
            List<Pawn> allColonists = Find.ColonistBar.GetColonistsInOrder().ToList();
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
