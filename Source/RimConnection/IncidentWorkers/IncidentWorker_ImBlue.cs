using RimWorld;
using UnityEngine;
using Verse;

// Copied from the Rimworld namespace, but made public
public class IncidentWorker_ImBlue : IncidentWorker
{
    protected override bool CanFireNowSub(IncidentParms parms)
    {
        return true;
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        Pawn randomColonist = Find.ColonistBar.GetColonistsInOrder().RandomElement();

        randomColonist.story.SkinColor = new Color(73, 66, 255);
        return true;
    }
}