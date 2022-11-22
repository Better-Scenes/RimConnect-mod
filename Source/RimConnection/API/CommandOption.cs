using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    [Serializable]
    public class CommandOption : ICloneable
    {
        public string actionHash { get; set; }
        public int localCooldownMs { get; set; }
        public int globalCooldownMs { get; set; }
        public int costSilverStore { get; set; }

        public IAction Action()
        {
                return ActionList.actionLookup[this.actionHash];
        }

        public object Clone()
        {
            return new CommandOption()
            {
                actionHash = actionHash,
                localCooldownMs = localCooldownMs,
                globalCooldownMs = globalCooldownMs,
                costSilverStore = costSilverStore
            };
        }
    }

    public class CommandOptionList : ICloneable
    {
        public List<CommandOption> commandOptions { get; set; }

        public object Clone()
        {
            return new CommandOptionList()
            {
                commandOptions = commandOptions
            };
        }
    }


    public class ValidCommandPostResponse
    {
        public bool success;
    }
}