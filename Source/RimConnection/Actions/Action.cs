using Verse;

namespace RimConnection { 
    public abstract class Action
    {
        public string name;
        public string description;
        public string category = "Other";
        public bool canSpawnMultiple = false;

        private static LetterDef twitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("TwitchEvent");
        private static LetterDef badTwitchEventLetterDef = DefDatabase<LetterDef>.GetNamed("DangerousTwitchEvent");

        public ValidCommand toApiCall(int id)
        {
            var command = new ValidCommand();
            command.modId = id.ToString();
            command.name = this.name;
            command.description = this.description;
            return command;
        }

        public abstract void execute(int amount);

    }
}
