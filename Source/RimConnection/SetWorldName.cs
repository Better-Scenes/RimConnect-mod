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
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            string worldName = Find.World.info.name;
            Log.Message($"World name is {worldName}");
            RimConnectAPI.UpdateWorld(worldName);
        }
    }
}
