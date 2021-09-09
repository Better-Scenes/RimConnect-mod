using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace RimConnection
{
    class MentalBreakAction : Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public MentalBreakDef mentalBreakDef;

        public MentalBreakAction(MentalBreakDef breakDef, string category = "Mental Break")
        {
            defName = breakDef.defName;
            defLabel = breakDef.defName;
            Name = defLabel;
            Description = "Trigger a mental break against a random colonist. These don't always work";
            ShouldShowAmount = true;
            Prefix = "Trigger";
            Category = category;
            mentalBreakDef = breakDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = 0;
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {
            IEnumerable<Pawn> randomisedColonists = Find.ColonistBar.GetColonistsInOrder().InRandomOrder();

            foreach(Pawn colonist in randomisedColonists)
            {
                // TODO revisit this logic to potentially make it more robust,
                // might just need to wait until refunds are available
                bool mentalBreakSucces =  mentalBreakDef.Worker.TryStart(colonist, "From RimConnect", false);
                if(mentalBreakSucces)
                {
                    String notificationMessage;
                    // I hope no viewer uses RC with the name "Poll"
                    if (boughtBy == "Poll")
                    {
                        notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has triggered {defLabel} for {colonist.Name}, hope your colony isn't a house of cards!";
                    }
                    else
                    {
                        notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {defLabel} for {colonist.Name}. hope your colony isn't a house of cards!";
                    }
                    AlertManager.BadEventNotification(notificationMessage);
                    return;
                }
            }
            AlertManager.NormalEventNotification($"{{0}} tried to buy {defLabel} but it failed. Currently there are no refunds :( ", boughtBy);
        }

        public ValidCommand ToApiCall(int id)
        {
            return new ValidCommand
            {
                name = Name,
                description = Description,
                category = Category,
                prefix = Prefix,
                actionHash = GenerateActionHash($"{mentalBreakDef.description}{defName}"),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
        }
    }
}
