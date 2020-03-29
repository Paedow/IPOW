using Godot;
using System;

namespace IPOW.Tiles
{
	public class Spawner : Tile
	{
		public override void _Ready()
		{
			BlockedLayer = IPOWLib.Pathing.MovementLayer.Ground;
		}

		public override Color GetMinimapColor()
		{
			return MinimapColors.MARKER;
		}
	}
}
