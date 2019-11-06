using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;

namespace RimConnection
{
    class SolarPanelAction : Action
    {
        public SolarPanelAction()
        {
            this.name = "Solar Panel";
            this.description = "Capture the sun and make it do the work for you";
            this.category = "Structures";
        }

        public override void execute(int amount)
        {
            var showMessage = true;
            for (int i = 0; i < amount; i++)
            {
                if(i > 0)
                {
                    showMessage = false;
                }
                DropPodManager.createDrop(ThingDefOf.SolarGenerator, 1, name, description, showMessage);
            }
        }
    }
}
