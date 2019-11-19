using System;

using RimWorld;
using Verse;

namespace RimConnection
{
    public class ItemAction: IAction
    {
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; } = "Item";
        public string prefix { get; set; } = "Spawn %amount%";
        public bool shouldShowAmount { get; set; } = true;

        private string defName;
        private string defLabel;

        public ItemAction(ThingDef itemDef)
        {
            defName = itemDef.defName;
            defLabel = itemDef.label;
            name = defLabel;
            description = "What it says on the tin";
        }

        public void Execute(int amount)
        {
            var itemDef = DefDatabase<ThingDef>.GetNamed(defName);

            DropPodManager.createDropFromDef(itemDef, amount, defLabel, $"Your viewers have given you {amount} {defLabel}s");
        }


        public ValidCommand ToApiCall(int id)
        {
            var command = new ValidCommand
            {
                modId = id.ToString(),
                name = name,
                description = description,
                category = category,
                prefix = prefix
                
            };
            return command;
        }
    }
}
