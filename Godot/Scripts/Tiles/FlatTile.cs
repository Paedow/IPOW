using Godot;
using System;
using IPOW;

namespace IPOW.Tiles
{
	public class FlatTile : Tile
	{
		public override void _Ready()
		{
			base.CanPlaceOn = true;
			/*MeshInstance mesh = this.GetNode<MeshInstance>(new NodePath("MeshInstance"));
			ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceMaterial(0);
			mat.SetShaderParam("tileOffset", new Vector2(tEST.X, tEST.Y));*/
		}

		public override void SetPosition(Grid3D parent, int x, int y)
		{
			bool x_odd = (x % 2) == 0;
			bool y_odd = (y % 2) == 0;
			MeshInstance bright = this.GetNode<MeshInstance>(new NodePath("MeshBright"));
			MeshInstance dark = this.GetNode<MeshInstance>(new NodePath("MeshDark"));
			if (x_odd == y_odd)
			{
				bright.Visible = false;
				dark.Visible = true;
			}
			else
			{
				bright.Visible = true;
				dark.Visible = false;
			}
			base.SetPosition(parent, x, y);
		}
	}
}
