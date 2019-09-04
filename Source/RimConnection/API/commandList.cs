using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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