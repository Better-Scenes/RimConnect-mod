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

        ValidCommand ToApiCall(int id);
        void Execute(int amount);
    }
}
