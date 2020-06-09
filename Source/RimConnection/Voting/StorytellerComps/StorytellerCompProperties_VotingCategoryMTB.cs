using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection.Voting.StorytellerComps
{
    public class StorytellerCompProperties_VotingCategoryMTB : StorytellerCompProperties
    {
        public StorytellerCompProperties_VotingCategoryMTB()
        {
            this.compClass = typeof(StorytellerComp_VotingCategoryMTB);
        }

        public float mtbDays = -1f;

        public SimpleCurve mtbDaysFactorByDaysPassedCurve;

        public IncidentCategoryDef category;
    }
}
