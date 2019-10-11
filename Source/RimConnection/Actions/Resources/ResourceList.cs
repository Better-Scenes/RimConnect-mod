using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class ResourceList
    {
        public static List<Action> resourceList = new List<Action>
        {
            new ResourcesCategoryAction(),
            new GlitterworldMedicineAction(),
            new GoldAction(),
            new HerbalMedicineAction(),
            new FineMealAction(),
            new LavishMealAction(),
            new NutrientPasteAction(),
            new SimpleMealAction(),
            new MedicineAction(),
            new PlasteelAction(),
            new SilverAction(),
            new SteelAction(),
            new UraniumAction(),
            new WoodAction()
        };
    }
}
