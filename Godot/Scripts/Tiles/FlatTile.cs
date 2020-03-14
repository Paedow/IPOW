using Godot;
using System;
using IPOW;

namespace IPOW.Tiles
{
	public class FlatTile : Tile
	{
		public override void _Ready()
		{
            /*MeshInstance mesh = this.GetNode<MeshInstance>(new NodePath("MeshInstance"));
			ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceMaterial(0);
			mat.SetShaderParam("tileOffset", new Vector2(tEST.X, tEST.Y));*/
		}
	}
}
