using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection
{
    public static class CustomList
    {
        public static List<Action> Actions;
        static CustomList()
        {
            Log.Message($"Loading custom RimConnect action list. {DefDatabase<CustomActionDef>.DefCount} entries found");
            Actions = new List<Action>();
            foreach (CustomActionDef actionDef in DefDatabase<CustomActionDef>.AllDefs)
            {
                Actions.Add(actionDef.GetAction());
                Actions.Sort((x,y) => x.Name.CompareTo(y.Name));
            }
        }
    }
}
