using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection.Voting.StorytellerComps
{
    public class StorytellerComp_VotingRandomMain : StorytellerComp
    {
        protected StorytellerCompProperties_VotingRandomMain Props
        {
            get
            {
                return (StorytellerCompProperties_VotingRandomMain)this.props;
            }
        }

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
        {
            List<IncidentDef> incidentDefs = new List<IncidentDef>();
            List<IncidentCategoryDef> categorySkips = new List<IncidentCategoryDef>();
            List<FiringIncident> firingIncidents = new List<FiringIncident>();

            if (Rand.MTBEventOccurs(this.Props.mtbDays, 60000f, 1000f) || RimConnectSettings.forceRandom)
            {
                bool failed = false;

                while (firingIncidents.Count < 4 && !failed)
                {
                    IncidentCategoryDef incidentCategoryDef = this.ChooseRandomCategory(target, categorySkips);
                    IncidentParms parms = this.GenerateParms(incidentCategoryDef, target);

                    IEnumerable<IncidentDef> usuableIncidentDefs = UsableIncidentsInCategory(incidentCategoryDef, parms)
                        .Where((x) => !incidentDefs.Contains(x));

                    if (usuableIncidentDefs.Count() < 1)
                    {
                        categorySkips.Add(incidentCategoryDef);
                    }

                    if (usuableIncidentDefs.TryRandomElementByWeight(new Func<IncidentDef, float>(base.IncidentChanceFinal), out IncidentDef incidentDef))
                    {
                        incidentDefs.Add(incidentDef);
                        firingIncidents.Add(new FiringIncident(incidentDef, this, parms));
                    }
                    else
                    {
                        failed = true;
                    }
                }

                VoteController voteController = Current.Game.GetComponent<VoteController>();

                if (voteController != null && firingIncidents.Count > 1)
                {
                    List<CustomVoteOption> voteOptions = new List<CustomVoteOption>();

                    foreach (FiringIncident firingIncident in firingIncidents)
                    {
                        voteOptions.Add(new CustomVoteOption(firingIncident.def.LabelCap, firingIncident));
                    }

                    voteController.RegisterNewPoll(new Poll(voteOptions));
                }
                else
                {
                    Log.Warning("Failed to generate vote");
                }
            }

            yield break;
        }

        private IncidentCategoryDef ChooseRandomCategory(IIncidentTarget target, List<IncidentCategoryDef> skipCategories)
        {
            if (!skipCategories.Contains(IncidentCategoryDefOf.ThreatBig))
            {
                int num = Find.TickManager.TicksGame - target.StoryState.LastThreatBigTick;
                if (target.StoryState.LastThreatBigTick >= 0 && (float)num > 60000f * this.Props.maxThreatBigIntervalDays)
                {
                    return IncidentCategoryDefOf.ThreatBig;
                }
            }
            return (from cw in this.Props.categoryWeights
                    where !skipCategories.Contains(cw.category)
                    select cw).RandomElementByWeight((IncidentCategoryEntry cw) => cw.weight).category;
        }

        public override IncidentParms GenerateParms(IncidentCategoryDef incCat, IIncidentTarget target)
        {
            IncidentParms incidentParms = StorytellerUtility.DefaultParmsNow(incCat, target);
            if (incidentParms.points >= 0f)
            {
                incidentParms.points *= this.Props.randomPointsFactorRange.RandomInRange;
            }
            return incidentParms;
        }
    }
}
