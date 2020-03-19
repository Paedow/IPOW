using Godot;
using System;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
	public class Hill : Tile
	{
		SurroundingTilemap[] tilemaps = new SurroundingTilemap[]{
			// Fully surrounded
			new SurroundingTilemapFull(true,true,true,true, true, true, true,true),
			// Border
			new SurroundingTilemap(true,true,true,false),
			new SurroundingTilemap(true, true,false,true),
			new SurroundingTilemap(true,false,true,true),
			new SurroundingTilemap(false,true,true,true),
			// Outer Corner
			new SurroundingTilemap(true,true,false,false),
			new SurroundingTilemap(false,true,false,true),
			new SurroundingTilemap(true,false,true,false),
			new SurroundingTilemap(false,false,true,true),
			// Inner Corner
			new SurroundingTilemapDiag(true, true, true, false),
			new SurroundingTilemapDiag(true, false,true,true),
			new SurroundingTilemapDiag(false,true,true,true),
			new SurroundingTilemapDiag(true,true,false,true)
		};
		int[] rotations = new int[]{
			0,
			0,90,-90,180,
			0,90,-90,180,
			0,-90,180,90
		};
		MeshInstance[] visibleObjects;

		MeshInstance surface, wall, corner1, corner2;
		Spatial rotator;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base.BlockedLayer = MovementLayer.ALL;

			surface = GetNode<MeshInstance>(new NodePath("MeshSurface"));
			wall = GetNode<MeshInstance>(new NodePath("Rotator/MeshWall"));
			corner1 = GetNode<MeshInstance>(new NodePath("Rotator/MeshCorner1"));
			corner2 = GetNode<MeshInstance>(new NodePath("Rotator/MeshCorner2"));
			rotator = GetNode<Spatial>(new NodePath("Rotator"));

			visibleObjects = new MeshInstance[]{
				surface, 
				wall,wall,wall,wall,
				corner1,corner1,corner1,corner1,
				corner2,corner2,corner2,corner2
			};
		}

		public override void GridReady(Grid3D parent, int x, int y)
		{
			int i = 0;
			for (i = 0; i < tilemaps.Length; i++)
			{
				if (tilemaps[i].MatchPattern(parent, x, y, typeof(Hill))) break;
			}
			if(i == tilemaps.Length) return;
			surface.Visible = false;
			wall.Visible = false;
			corner1.Visible = false;
			corner2.Visible = false;

			rotator.RotationDegrees = new Vector3(0,rotations[i],0);
			visibleObjects[i].Visible = true;
		}
	}
}
