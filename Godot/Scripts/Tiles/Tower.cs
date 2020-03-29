using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace IPOW.Tiles
{
	public class Tower : Tile
	{
		public Tower()
		{
			BlockedLayer = IPOWLib.Pathing.MovementLayer.Ground | IPOWLib.Pathing.MovementLayer.Water;
		}

		public override string[] GetCommands()
		{
			return new string[] { "Upgrade", "Remove" };
		}

		public override void RunCommand(string cmd)
		{
			if(cmd == "Remove")
			{
				if(LastTile != null)
				{
					ParentGrid.SetTile(LastTile, X, Y);
				}
			}
		}

		public override Color GetMinimapColor()
		{
			return MinimapColors.TOWER;
		}
	}
}
