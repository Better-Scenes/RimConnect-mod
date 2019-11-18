using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class GearList
    {
        public static List<IAction> gearList = new List<IAction>
        {
            new GearCategoryAction(),
            new RandomApparelAction(),
            new RandomWeaponAction(),
            new ShieldBeltAction()
        };
    }
}
