using Godot;
using System;
using IPOW.Tiles;

public class World : Node
{
	Grid3D grid;

	public override void _Ready()
	{
		grid = new Grid3D();
		this.AddChild(grid);
	}


}
