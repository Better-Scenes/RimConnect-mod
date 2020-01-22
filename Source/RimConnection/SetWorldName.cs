using System;
using Verse;

namespace RimConnection
{
    class SetWorldName : GameComponent
    {
        public SetWorldName(Game game)
        {
        }

        public SetWorldName()
        {
            Log.Message($"World name is {Find.World.info.name}");
        }

        public override void FinalizeInit()
        {
            Log.Message($"World name is {Find.World.info.name}");
        }
    }
}
