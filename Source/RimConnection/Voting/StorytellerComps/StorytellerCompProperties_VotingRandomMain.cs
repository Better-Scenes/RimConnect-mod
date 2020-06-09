using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RimConnection.Voting.StorytellerComps
{
    public class StorytellerCompProperties_VotingRandomMain : StorytellerCompProperties
    {
        public StorytellerCompProperties_VotingRandomMain()
        {
            this.compClass = typeof(StorytellerComp_VotingRandomMain);
        }

        public float mtbDays;

        public List<IncidentCategoryEntry> categoryWeights = new List<IncidentCategoryEntry>();

        public float maxThreatBigIntervalDays = 99999f;

        public FloatRange randomPointsFactorRange = new FloatRange(0.5f, 1.5f);

        public bool skipThreatBigIfRaidBeacon;
    }
}