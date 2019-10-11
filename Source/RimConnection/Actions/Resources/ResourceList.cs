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
            new FineMealAction(),
            new GlitterworldMedicineAction(),
            new GoldAction(),
            new HerbalMedicineAction(),
            new LavishMealAction(),
            new MedicineAction(),
            new NutrientPasteAction(),
            new PlasteelAction(),
            new SilverAction(),
            new SimpleMealAction(),
            new SteelAction(),
            new UraniumAction(),
            new WoodAction()
        };
    }
}
