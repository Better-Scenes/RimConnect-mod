using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace RimConnection
{
    public class CommandOption
    {
        public string ActionHash { get; set; }
        public int LocalCooldownMs { get; set; }
        public int GlobalCooldownMs { get; set; }
        public int CostSilverStore { get; set; }

        public IAction Action
        {
            get
            {
                return ActionList.actionLookup[this.ActionHash];
            }
        }
    }

    public class CommandOptionList
    {
        public List<CommandOption> CommandOptions { get; set; }
    }
}