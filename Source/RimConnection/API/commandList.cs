using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    public class Command
    {
        public string actionHash { get; set; }
        public int amount { get; set; }
        public string boughtBy { get; set; }
    }

    public class CommandList
    {
        public List<Command> commands { get; set; }
    }
}