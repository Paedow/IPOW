using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOW.Tiles
{
	public class Tower : Tile
	{
		public Tower()
		{
			BlockedLayer = IPOWLib.Pathing.MovementLayer.Ground | IPOWLib.Pathing.MovementLayer.Water;
		}
	}
}
