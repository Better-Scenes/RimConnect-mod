using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection.Voting
{
    public class StorytellerComp_VotingCategoryMTB : StorytellerComp
    {
        protected StorytellerCompProperties_VotingCategoryMTB Props
        {
            get
            {
                Log.Message("Casting Props");
                return (StorytellerCompProperties_VotingCategoryMTB)this.props;
            }
        }

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            Log.Message("Making new list");
            List<FiringIncident> incidents = new List<FiringIncident>();

            float num = this.Props.mtbDays;
            if (this.Props.mtbDaysFactorByDaysPassedCurve != null)
            {
                num *= this.Props.mtbDaysFactorByDaysPassedCurve.Evaluate(GenDate.DaysPassedFloat);
            }
            if (Rand.MTBEventOccurs(num, 60000f, 1000f))
            {
                bool failed = false;

                while (incidents.Count < 4 && !failed)
                {
                    Log.Message("Running Loop");
                    IncidentParms parms = this.GenerateParms(this.Props.category, target);
                    IncidentDef def;
                    if (base.UsableIncidentsInCategory(this.Props.category, parms)
                        .Where((y) => incidents.Find((x) => x.def.defName != y.defName) == null)
                        .TryRandomElementByWeight(
                            (IncidentDef incDef) =>
                                base.IncidentChanceFinal(incDef), out def)
                        )
                    {
                        Log.Message("Adding incident");
                        incidents.Add(new FiringIncident(def, this, parms));
                    }
                    else
                    {
                        failed = true;
                    }
                }


                if (incidents.Count > 2)
                {
                    // Create a Vote!

                    foreach (FiringIncident incident in incidents)
                    {
                        Log.Message($"Generated FiringIncident with def {incident.def.defName}");
                    }
                }
            }

            yield break;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.Props.category;
        }
    }
}
