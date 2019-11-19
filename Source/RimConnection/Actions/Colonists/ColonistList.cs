using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public static class ColonistList
    {
        public static List<IAction> colonistList = new List<IAction>
        {
            new AwfulColonistAction(),
            new DefaultColonistAction(),
            new GoodColonistAction()
        };
    }
}
