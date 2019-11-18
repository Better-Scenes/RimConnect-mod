using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
namespace RimConnection
{
    public class Command
    {
        public string id { get; set; }
        public int amount { get; set; }
    }

    public class CommandList
    {
        public List<Command> commands { get; set; }
    }
}