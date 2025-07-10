using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RimWorld;
using Verse;

namespace RimConnection
{
    public class HeDiffAction: Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public HediffDef thingDef;

        public HeDiffAction(HediffDef HeDiffDef, string category = "Health effect")
        {
            defName = HeDiffDef.defName;
            defLabel = HeDiffDef.label;
            Name = defLabel;
            Description = HeDiffDef.description;
            ShouldShowAmount = true;
            Prefix = "Trigger";
            Category = category;
            thingDef = HeDiffDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = 0;
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {
            //HediffDef heDiffDef = DefDatabase<HediffDef>.GetNamed(thingDef.defName);
            var heDiffGiver = PawnKindDefOf.Colonist.RaceProps
                .hediffGiverSets
                .SelectMany(set => set.hediffGivers)
                .FirstOrDefault(hg => hg.hediff == this.thingDef);
            if (heDiffGiver == null)
            {
                Log.Error($"[HeDiffAction] No HediffGiver found for {thingDef.defName}");
                return;
            }


            String notificationMessage;

            // I hope no viewer uses RC with the name "Poll"
            if (boughtBy == "Poll")
            {
                notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has triggered {defLabel} on random colonists.";
            }
            else
            {
                notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {defLabel} for a random colonist.";
            }

            var colonists = Find.ColonistBar.GetColonistsInOrder().Where(colonist => !colonist.Dead);

            var randomColonists = colonists.InRandomOrder().Take(amount);
            foreach (Pawn colonist in randomColonists)
            {
                heDiffGiver.TryApply(colonist);
            }

            string additionalMessage = "";
            if(amount == 1)
            {
                additionalMessage = $"{randomColonists.First().Name.ToStringFull} has been given {heDiffGiver.hediff.label}.";
            } else
            {
                IEnumerable<String> colonistNames = randomColonists.Select((Pawn colonist) => colonist.Name.ToStringFull);
                additionalMessage = $"{String.Join(", ", colonistNames)} have been given {heDiffGiver.hediff.label}.";
            }

            AlertManager.BadEventNotification($"{notificationMessage}\n\n{additionalMessage}");
        }

        public ValidCommand ToApiCall(int id)
        {
            return new ValidCommand
            {
                name = Name,
                description = Description,
                category = Category,
                prefix = Prefix,
                actionHash = GenerateActionHash($"{thingDef.description}{defName}"),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
        }
    }
}