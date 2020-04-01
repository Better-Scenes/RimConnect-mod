using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RimConnection { 
    public abstract class Action : IAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } = "Other";
        public string Prefix { get; set; } = "Spawn";
        public bool ShouldShowAmount { get; set; } = false;
        public int LocalCooldownMs { get; set; } = 120000;
        public int GlobalCooldownMs { get; set; } = 60000;
        public int CostSilverStore { get; set; } = -1;
        public string BitStoreSKU { get; set; } = "";
        public string ActionHash { get; set; } = "";

        public string GenerateActionHash(string extraData = "")
        {
            var input = $"{Name}{Description}{Category}{Prefix}{extraData}";
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.Append(b.ToString("X2"));

            ActionHash = sb.ToString();
            return ActionHash;
        }

        public ValidCommand ToApiCall()
        {
            return new ValidCommand
            {
                name = Name,
                description = Description,
                category = Category,
                prefix = Prefix,
                shouldShowAmount = ShouldShowAmount,
                actionHash = GenerateActionHash(),
                localCooldownMs = LocalCooldownMs,
                globalCooldownMs = GlobalCooldownMs,
                costSilverStore = CostSilverStore,
                bitStoreSKU = BitStoreSKU
            };
        }



        public abstract void Execute(int amount, string boughtBy);


    }
}
