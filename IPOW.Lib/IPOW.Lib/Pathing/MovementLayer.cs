using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOWLib.Pathing
{
    public enum MovementLayer
    {
        ALL = 255,
        Ground = 1,
        Air = 2,
        Underground = 4,
        Water = 8
    }
}
