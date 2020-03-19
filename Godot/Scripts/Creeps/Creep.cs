using Godot;
using System;
using IPOW.Tiles;
using IPOWLib.Pathing;

namespace IPOW.Creeps
{
	public class Creep : Spatial
	{
		public MovementLayer Layer { get; protected set; } = MovementLayer.Ground;
		public float MovementSpeed { get; protected set; } = 1;
		public bool Blocked { get; protected set; } = false;
		World world;
		Grid3D grid3d;
		IGrid grid;
		SplinePath path;
		float walkedDistance;
		bool shouldCalcNew = false;
		PointI[] points;
		PathFinder pathFinder = null;
		static PackedScene excplamation = null;
		Spatial exclamationInst;

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
			if(excplamation == null)
			{
				excplamation = GD.Load<PackedScene>("res://Scenes/Objects/Exclamation.tscn");
			}
			exclamationInst = (Spatial)excplamation.Instance();
			AddChild(exclamationInst);
			exclamationInst.Visible = false;
		}

		public override void _Process(float delta)
		{
			if (shouldCalcNew && ((int)walkedDistance < (int)(walkedDistance + delta * MovementSpeed) || path == null))
			{
				shouldCalcNew = false;
				float gridSize = this.grid3d.GetGridSize();
				PointI nextGrid = new PointI((int)(Translation.x / gridSize), (int)(Translation.z / gridSize));
				if (pathFinder.FindPath(out points, nextGrid))
				{
					if (points.Length > 1) path = new SplinePath(points, grid3d.GetGridSize(), InterpolationType.Qubic);
					Blocked = false;
					exclamationInst.Visible = false;
				}
				else
				{
					Blocked = true;
					path = null;
					walkedDistance = 0;
					exclamationInst.Visible = true;
				}
				walkedDistance = 0;
			}
			if (path != null)
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
			shouldCalcNew = true;
			this.pathFinder = pathFinder;
		}
	}
}
