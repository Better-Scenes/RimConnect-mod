using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public class ValidCommand
    {
        public string modId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ValidCommandList
    {
        public List<ValidCommand> validCommands { get; set; }
    }
}