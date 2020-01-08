using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RimConnection
{
    public class AuthMod
    {
        public string secret = RimConnectSettings.secret;
    }

    public class AuthModResponse
    {
        public string token { get; set; }
    }
}