using Verse;

namespace RimConnection { 
    public abstract class Action : IAction
    {
        public string actionHash { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; } = "Other";
        public string prefix { get; set; } = "Spawn";
        public bool shouldShowAmount { get; set; } = false;

        public ValidCommand ToApiCall(int id)
        {
            var command = new ValidCommand
            {
                modId = id.ToString(),
                name = name,
                description = description,
                category = category,
                prefix = prefix,
                shouldShowAmount = shouldShowAmount
            };
            return command;
        }

        public abstract void Execute(int amount);

    }
}
