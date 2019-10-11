using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class StructureList
    {
        public static List<Action> structureList = new List<Action>
        {
            new StructuresCategoryAction(),
            new BatteryAction(),
            new CoolerAction(),
            new HeaterAction(),
            new SolarPanelAction(),
        };
    }
}
