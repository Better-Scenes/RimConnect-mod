using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace RimConnection
{
    class WeatherCategoryAction : Action
    {
        public WeathersCategoryAction()
        {
            this.name = "Category: Weather";
            this.description = "This is just a category, it doesn't do anything";
            this.category = "Category";
        }

        public override void execute(int amount)
        {
            return;
        }
    }
}
