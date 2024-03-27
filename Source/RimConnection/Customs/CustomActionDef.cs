using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace RimConnection
{
    public class CustomActionDef : Def
    {
        readonly Action Action;

        readonly string name = "Custom Action";
        readonly new string description = "Does something.";
        readonly string category = "Other";
        readonly string prefix = "Spawn";
        readonly bool shouldShowAmount = false;
        readonly int localCooldownMs = 120000;
        readonly int globalCooldownMs = 60000;
        readonly int costSilverStore = 0;
        readonly string bitStoreSKU = "";
        readonly string actionHash = "";

        public Action GetAction()
        {
            Action.Name = name;
            Action.Description = description;
            Action.Category = category;
            Action.Prefix = prefix;
            Action.ShouldShowAmount = shouldShowAmount;
            Action.LocalCooldownMs = localCooldownMs;
            Action.GlobalCooldownMs = globalCooldownMs;
            Action.CostSilverStore = costSilverStore;
            Action.BitStoreSKU = bitStoreSKU;
            Action.ActionHash = actionHash;

            return Action;
        }
    }
}
