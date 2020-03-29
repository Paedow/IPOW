using Godot;
using System;
using IPOW.Tiles;
using System.Collections.Generic;
using IPOWLib.Pathing;
using IPOW.Creeps;

public class World : Node
{
	public Grid3D Grid { get; private set; }
	public List<Creep> Creeps { get; private set; }

	public override void _Ready()
	{
		Creeps = new List<Creep>();
	}

	public void SpawnCreep(Creep creep, int x, int y)
	{
		float f = Grid.GetGridSize();
		creep.Translation = new Vector3(f * (x+.5f), 0, f * (y+.5f));
		creep.Setup(this);
		Creeps.Add(creep);
		this.AddChild(creep);
	}

	public void UpdatePath(PathFinder pathFinder)
	{
		for(int i = 0; i < Creeps.Count;i++)
		{
			Creeps[i].PathsUpdated(pathFinder);
		}
	}

	public void GridChanged()
	{
		for (int i = 0; i < Creeps.Count; i++)
		{
			Creeps[i].GridChanged();
		}
	}
}
