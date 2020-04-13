using RimConnection;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimConnection
{
    public class IncidentWorker_ImBlue : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            BlueSkinTracker blueSkinTracker = Current.Game.GetComponent<BlueSkinTracker>();
            List<Pawn> colonists = Find.ColonistBar.GetColonistsInOrder();
            blueSkinTracker.bluePawns = colonists;

            colonists[0].Drawer.renderer.graphics.ResolveAllGraphics();
            Log.Message("Make sure that the execute worker gets after resolve graphcis and before get skin colour");
            var a = colonists[0].story.SkinColor;
            return true;
        }
    }
}