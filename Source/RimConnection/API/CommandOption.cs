using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    [DataContract]
    public class CommandOption : ICloneable
    {
        [DataMember(Name="actionHash")]
        public string actionHash { get; set; }
        [DataMember(Name="localCooldownMs")]
        public int localCooldownMs { get; set; }
        [DataMember(Name="globalCooldownMs")]
        public int globalCooldownMs { get; set; }
        [DataMember(Name="costSilverStore")]
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