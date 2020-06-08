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
                return (StorytellerCompProperties_VotingCategoryMTB)this.props;
            }
        }

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            List<FiringIncident> incidents = new List<FiringIncident>();
            List<IncidentDef> defs = new List<IncidentDef>();

            float num = this.Props.mtbDays;
            if (this.Props.mtbDaysFactorByDaysPassedCurve != null)
            {
                num *= this.Props.mtbDaysFactorByDaysPassedCurve.Evaluate(GenDate.DaysPassedFloat);
            }
            if (Rand.MTBEventOccurs(num, 60000f, 1000f) || RimConnectSettings.forceRandom)
            {
                bool failed = false;

                while (defs.Count < 4 && !failed)
                {
                    Log.Message("Running Loop");
                    IncidentParms parms = this.GenerateParms(this.Props.category, target);
                    IncidentDef def;
                    if (base.UsableIncidentsInCategory(this.Props.category, parms)
                        .Where((y) => !defs.Contains(y))
                        .TryRandomElementByWeight(
                            (IncidentDef incDef) =>
                                base.IncidentChanceFinal(incDef), out def)
                        )
                    {
                        Log.Message("Adding incident");
                        defs.Add(def);
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
            else
            {
                Log.Message("Rand does not occur", true);
            }

            yield break;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.Props.category;
        }
    }
}
