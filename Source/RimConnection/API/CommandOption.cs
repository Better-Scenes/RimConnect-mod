using System;
using System.Collections.Generic;
using Verse;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    public class CommandOption : ICloneable, IExposable
    {
        private string _actionHash;
        private int _localCooldownMs;
        private int _globalCooldownMs;
        private int _costSilverStore;

        public string actionHash { get => _actionHash; set => _actionHash = value; }
        public int localCooldownMs { get => _localCooldownMs; set => _localCooldownMs = value; }
        public int globalCooldownMs { get => _globalCooldownMs; set => _globalCooldownMs = value; }
        public int costSilverStore { get => _costSilverStore; set => _costSilverStore = value; }

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

        public void ExposeData()
        {
            Scribe_Values.Look(ref _actionHash, "ActionHash");
            Scribe_Values.Look(ref _localCooldownMs, "localCooldownMs");
            Scribe_Values.Look(ref _globalCooldownMs, "globalCooldownMs");
            Scribe_Values.Look(ref _costSilverStore, "costSilverStore");
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