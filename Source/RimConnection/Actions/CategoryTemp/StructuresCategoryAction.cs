using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class StructuresCategoryAction : Action
    {
        public StructuresCategoryAction()
        {
            this.name = "Category: Structures";
            this.description = "This is just a category, it doesn't do anything";
            this.category = "Category";
        }

        public override void execute(int amount)
        {
            return;
        }
    }
}
