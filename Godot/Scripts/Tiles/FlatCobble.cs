using Godot;
using System;
using IPOW;

namespace IPOW.Tiles
{
	public class FlatCobble : Tile
	{
		public override void _Ready()
		{
			base.CanPlaceOn = true;
		}

		public override void SetPosition(Grid3D parent, int x, int y)
		{
			base.SetPosition(parent, x, y);
		}
	}
}
