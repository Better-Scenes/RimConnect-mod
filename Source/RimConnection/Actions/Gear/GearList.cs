using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class GearList
    {
        public static List<Action> gearList = new List<Action>
        {
            new GearCategoryAction(),
            new RandomApparelAction(),
            new RandomWeaponAction(),
            new ShieldBeltAction()
        };
    }
}
