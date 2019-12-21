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
        public int localCooldownMs { get; set; } = 120000;
        public int globalCooldownMs { get; set; } = 60000;
        public int costSilverStore { get; set; } = -1;
        public string bitStoreSKU { get; set; } = "";
        public string actionHash { get; set; } = "";

        public string GenerateActionHash(string extraData = "")
        {
            var input = $"{name}{description}{category}{prefix}{extraData}";
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.Append(b.ToString("X2"));

            actionHash = sb.ToString();
            return actionHash;
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
                actionHash = GenerateActionHash(),
                localCooldownMs = localCooldownMs,
                globalCooldownMs = globalCooldownMs,
                costSilverStore = costSilverStore,
                bitStoreSKU = bitStoreSKU
            };
            return command;
        }



        public abstract void Execute(int amount);


    }
}
