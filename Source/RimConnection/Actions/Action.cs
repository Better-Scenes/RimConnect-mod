using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RimConnection { 
    public abstract class Action : IAction
    {
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; } = "Other";
        public string prefix { get; set; } = "Spawn";
        public bool shouldShowAmount { get; set; } = false;

        public string ActionHash()
        {
                var input = $"{name}{description}{category}{prefix}";
                var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Concat(hash.Select(b => b.ToString("X2")));
        }

        public ValidCommand ToApiCall()
        {
            var command = new ValidCommand
            {
                name = name,
                description = description,
                category = category,
                prefix = prefix,
                shouldShowAmount = shouldShowAmount,
                actionHash = ActionHash()
            };
            return command;
        }

        public abstract void Execute(int amount);


    }
}
