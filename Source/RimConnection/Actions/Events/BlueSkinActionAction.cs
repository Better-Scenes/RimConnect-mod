using RimWorld;
using Verse;

namespace RimConnection
{
    class BlueSkinAction : Action
    {
        public BlueSkinAction()
        {
            Name = "I'm Blue (Da ba dee, da ba die)";
            Description = "Da ba dee, da ba die, Da ba dee, da ba die, Da ba dee, da ba die";
            Category = "Event";
            Prefix = "Trigger";
        }

        public override void Execute(int amount, string boughtBy)
        {
            var currentMap = Find.CurrentMap;

            var parms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.Misc, currentMap);
            var blueWorker = new IncidentWorker_ImBlue();
            blueWorker.def = IncidentDef.Named("ImBlue");

            blueWorker.TryExecute(parms);

            AlertManager.NormalEventNotification(@"Yo, listen up here's the story
About a little guy that lives in a blue world
And all day and all night and everything he sees is just blue
Like him inside and outside

Blue his house
With a blue little window
And a blue corvette
And everything is blue for him
And himself and everybody around
'Cause he ain't got nobody to listen...

I'm blue
Da ba dee, da ba die
Da ba dee, da ba die
Da ba dee, da ba die

Your viewers have turned some of your colonists blue
");

        }
    }
}
