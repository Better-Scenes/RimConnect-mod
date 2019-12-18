using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public interface IAction
    {
        string name { get; set; }
        string description { get; set; }
        string category { get; set; }
        string prefix { get; set; }
        bool shouldShowAmount { get; set; }
        int localCooldownMs { get; set; }
        int globalCooldownMs { get; set; }
        int costSilverStore { get; set; }
        string bitStoreSKU { get; set; }

        ValidCommand ToApiCall();
        void Execute(int amount);
        string ActionHash();
    }
}
