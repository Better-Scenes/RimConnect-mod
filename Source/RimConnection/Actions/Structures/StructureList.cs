using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class StructureList
    {
        public static List<IAction> structureList = new List<IAction>
        {
            new StructuresCategoryAction(),
            new BatteryAction(),
            new CoolerAction(),
            new HeaterAction(),
            new SolarPanelAction(),
        };
    }
}
