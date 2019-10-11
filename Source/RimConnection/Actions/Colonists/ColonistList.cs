using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class ColonistList
    {
        public static List<Action> colonistList = new List<Action>
        {
            new ColonistCategoryAction(),
            new AwfulColonistAction(),
            new DefaultColonistAction(),
            new GoodColonistAction()
        };
    }
}
