using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace RimConnection
{
    class InspirationAction : Action, IAction
    {
        private readonly string defName;
        private readonly string defLabel;
        public InspirationDef inspirationDef;

        public InspirationAction(InspirationDef inspDef, string category = "Colonist State")
        {
            defName = inspDef.defName;
            defLabel = inspDef.label;
            Name = defLabel;
            Description = "Give the best colonist at the job this inspiration";
            ShouldShowAmount = true;
            Prefix = "Trigger";
            Category = category;
            inspirationDef = inspDef;
            LocalCooldownMs = 30000;
            GlobalCooldownMs = 15000;
            CostSilverStore = 300;
            BitStoreSKU = "";
        }

        public override void Execute(int amount, string boughtBy)
        {


            IEnumerable<Pawn> colonists = Find.ColonistBar.GetColonistsInOrder();
            IEnumerable<Pawn> orderedColonists;

            if(inspirationDef.associatedSkills == null)
            {
                orderedColonists = colonists.InRandomOrder();
            }
            else
            {
                // Just grab the first skill, current inspirations only appear to have 0 or 1 in the list
                // Worst case it just won't take all skills into account
                SkillDef skillDef = inspirationDef.associatedSkills[0];
                orderedColonists = colonists.OrderByDescending(colonist => colonist.skills.GetSkill(skillDef).Level);
            }

            foreach (Pawn colonist in orderedColonists)
            {
                bool colonistInspirationSet = colonist.mindState.inspirationHandler.TryStartInspiration(inspirationDef);
                if(colonistInspirationSet)
                {
                    String notificationMessage;
                    // I hope no viewer uses RC with the name "Poll"
                    if (boughtBy == "Poll")
                    {
                        notificationMessage = $"<color=#9147ff>By popular opinion</color>, your channel has triggered {defLabel} for {colonist.Name}, hopefully it helps!";
                    }
                    else
                    {
                        notificationMessage = $"<color=#9147ff>{boughtBy}</color> purchased {defLabel} for {colonist.Name}. Enjoy!!";
                    }
                    AlertManager.NormalEventNotification(notificationMessage);
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
                actionHash = GenerateActionHash($"{inspirationDef.description}{defName}"),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
        }
    }
}
