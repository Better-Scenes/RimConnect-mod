using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimConnection
{
    class BlueSkinTracker : GameComponent
    {
        public List<Pawn> bluePawns = new List<Pawn>();

        public BlueSkinTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref bluePawns, "bluePawns", LookMode.Reference);
        }
    }
}
