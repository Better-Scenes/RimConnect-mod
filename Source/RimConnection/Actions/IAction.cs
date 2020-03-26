using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public interface IAction
    {
        string Name { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        string Prefix { get; set; }
        bool shouldShowAmount { get; set; }
        int LocalCooldownMs { get; set; }
        int GlobalCooldownMs { get; set; }
        int CostSilverStore { get; set; }
        string BitStoreSKU { get; set; }
        string ActionHash { get; set; }

        ValidCommand ToApiCall();
        void Execute(int amount);
        string GenerateActionHash(string extraData = "");
    }
}
