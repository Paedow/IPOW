using Godot;
using System;
using IPOW.Tiles;
using Pathing;

public class Creep : Spatial
{
	public IPOW.Pathing.MovementLayer Layer { get; protected set; } = IPOW.Pathing.MovementLayer.Ground;
	public float MovementSpeed { get; protected set; } = 1;
	World world;
	Grid3D grid3d;
	IGrid grid;
	Pathing.SplinePath path;
	float walkedDistance;

	public virtual void Setup(World world)
	{
		this.world = world;
		this.grid3d = world.Grid;
		this.grid = grid3d.GetGrid(Layer);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		path = null;
		walkedDistance = 0;
	}

	public override void _Process(float delta)
	{
		if(path != null)
		{
			Vector2 pos = path.GetPoint(walkedDistance);
			walkedDistance += delta * MovementSpeed;
			this.Translation = new Vector3(pos.x, 0, pos.y);
		}
	}

	public virtual void GridChanged()
	{
		this.grid3d = world.Grid;
		this.grid = grid3d.GetGrid(Layer);
	}

	public virtual void PathsUpdated(PathFinder pathFinder)
	{
		PointI[] points;
		pathFinder.FindPath(out points, new PointI(0, 0));
		path = new SplinePath(points, grid3d.GetGridSize(), InterpolationType.Qubic);
		walkedDistance = 0;
	}
}
