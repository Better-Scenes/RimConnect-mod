using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    public class ValidCommand
    {
        public string actionHash { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string prefix { get; set; }
        public bool shouldShowAmount { get; set; }
    }

    public class ValidCommandList
    {
        public List<ValidCommand> validCommands { get; set; }
    }
}