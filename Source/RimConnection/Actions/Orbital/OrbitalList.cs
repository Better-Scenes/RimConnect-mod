using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimConnection
{
    class OrbitalList
    {
        public static List<IAction> orbitalList = new List<IAction>
        {
            new TornadoAction(),
            new OrbitalBombardmentAction(),
            new OrbitalPowerBeamAction()
        };
    }
}
