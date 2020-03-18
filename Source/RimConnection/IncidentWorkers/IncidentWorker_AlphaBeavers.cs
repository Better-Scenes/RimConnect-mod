using RimWorld;
using UnityEngine;
using Verse;

// Copied from the Rimworld namespace, but made public
public class IncidentWorker_Alphabeavers : IncidentWorker
{
    private static readonly FloatRange CountPerColonistRange = new FloatRange(1f, 1.5f);

    private const int MinCount = 1;

    private const int MaxCount = 10;

    protected override bool CanFireNowSub(IncidentParms parms)
    {
        if (!base.CanFireNowSub(parms))
        {
            return false;
        }
        Map map = (Map)parms.target;
        IntVec3 result;
        return RCellFinder.TryFindRandomPawnEntryCell(out result, map, CellFinder.EdgeRoadChance_Animal);
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        Map map = (Map)parms.target;
        PawnKindDef alphabeaver = PawnKindDefOf.Alphabeaver;
        if (!RCellFinder.TryFindRandomPawnEntryCell(out IntVec3 result, map, CellFinder.EdgeRoadChance_Animal))
        {
            return false;
        }
        int freeColonistsCount = map.mapPawns.FreeColonistsCount;
        float randomInRange = CountPerColonistRange.RandomInRange;
        float f = (float)freeColonistsCount * randomInRange;
        int num = Mathf.Clamp(GenMath.RoundRandom(f), 1, 10);
        for (int i = 0; i < num; i++)
        {
            IntVec3 loc = CellFinder.RandomClosewalkCellNear(result, map, 10);
            Pawn newThing = PawnGenerator.GeneratePawn(alphabeaver);
            Pawn pawn = (Pawn)GenSpawn.Spawn(newThing, loc, map);
            pawn.needs.food.CurLevelPercentage = 1f;
        }
        Find.LetterStack.ReceiveLetter("LetterLabelBeaversArrived".Translate(), "BeaversArrived".Translate(), LetterDefOf.ThreatSmall, new TargetInfo(result, map));
        return true;
    }
}