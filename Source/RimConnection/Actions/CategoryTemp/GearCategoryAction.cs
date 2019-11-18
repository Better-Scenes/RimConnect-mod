using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class GearCategoryAction : Action
    {
        public GearCategoryAction()
        {
            this.name = "== Category: Gear ==";
            this.description = "This is just a category, it doesn't do anything";
            this.category = "Category";
        }

        public override void Execute(int amount)
        {
            return;
        }
    }
}
