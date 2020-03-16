using Godot;
using System;
using System.Collections.Generic;
using IPOWLib.Pathing;

namespace Pathing
{
	public class TestGrid : Node2D, IGrid
	{
		int gridSize = 20;
		int SWidth = 800;
		int SHeight = 600;
		int fWidth, fHeight;

		bool[,] field;

		Color lineColor, blockColor, startColor, endColor, pathColor;
		PointI[] start, end;
		List<PointI[]> paths = new List<PointI[]>();
		Font font;
		List<SplinePath> splinePaths = new List<SplinePath>();
		AsyncPathUpdater pathUpdater;
		uint pathversion = 0;

		public override void _Ready()
		{
			lineColor = Color.Color8(0, 100, 0, 255);
			pathColor = Color.Color8(0, 255, 0, 255);
			blockColor = Color.Color8(255, 255, 0, 255);
			startColor = Color.Color8(0, 0, 255, 255);
			endColor = Color.Color8(255, 0, 0, 255);
			fWidth = SWidth / gridSize;
			fHeight = SHeight / gridSize;
			field = new bool[fWidth, fHeight];

			start = new PointI[] { new PointI(1, 1), new PointI(18, 2), new PointI(37, 27), new PointI(5, 27) };
			end = new PointI[] { new PointI(19, 14), new PointI(1, 14) };

			pathUpdater = new AsyncPathUpdater(this);
		}

		public bool FieldBlocked(int x, int y)
		{
			if (!InField(x, y)) return true;
			return field[x, y];
		}

		public int GetGridWidth()
		{
			return fWidth;
		}

		public int GetGridHeight()
		{
			return fHeight;
		}

		public bool InField(int x, int y)
		{
			if (x < 0 || x >= fWidth) return false;
			if (y < 0 || y >= fHeight) return false;
			return true;
		}

		public override void _Draw()
		{
			for (int x = 0; x <= SWidth; x += gridSize)
			{
				DrawLine(new Vector2(x, 0), new Vector2(x, SHeight), lineColor);
			}
			for (int y = 0; y <= SHeight; y += gridSize)
			{
				DrawLine(new Vector2(0, y), new Vector2(SWidth, y), lineColor);
			}

			for (int x = 0; x < SWidth / gridSize; x++)
			{
				for (int y = 0; y < SHeight / gridSize; y++)
				{
					if (FieldBlocked(x, y))
					{
						Rect2 rect = new Rect2(x * gridSize, y * gridSize, gridSize, gridSize);
						DrawRect(rect, blockColor, true);
					}
				}
			}

			/*foreach(var path in paths)
			{
				for(int i = 0; i < path.Length-1;i++)
				{
					DrawLine(getCenter(path[i]), getCenter(path[i+1]), pathColor);
				}
			}*/
			foreach (SplinePath path in splinePaths)
			{
				float dx = .1f;
				for (float i = 0; i < path.Length - 1; i += dx)
				{
					Vector2 p1 = path.GetPoint(i);
					Vector2 p2 = path.GetPoint(i + dx);
					DrawLine(p1, p2, pathColor);
				}
			}

			foreach (PointI startP in start)
				DrawCircle(getCenter(startP), gridSize / 2, startColor);
			foreach (PointI endP in end)
				DrawCircle(getCenter(endP), gridSize / 2, endColor);
		}

		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouse)
			{
				InputEventMouse mouseEvent = (InputEventMouse)@event;
				int x = (int)mouseEvent.GlobalPosition.x / gridSize;
				int y = (int)mouseEvent.GlobalPosition.y / gridSize;
				if (InField(x, y) && mouseEvent.ButtonMask == 1)
				{
					field[x, y] = true;
					UpdateGrid();
				}
				if (InField(x, y) && mouseEvent.ButtonMask == 2)
				{
					field[x, y] = false;
					UpdateGrid();
				}
			}
		}

		public void UpdateGrid()
		{
			/*PathFinder pf = new PathFinder(this, true);
			pf.Reset();
			foreach(PointI endP in end)
				pf.CalcHops(endP);*/
			pathUpdater.Update(end);
			/*paths.Clear();
			splinePaths.Clear();
			foreach(PointI startP in start)
			{
				PointI[] path;
				pf.FindPath(out path, startP);
				paths.Add(path);
				splinePaths.Add(new Spline.Path(path, gridSize, new Vector2(1,1)*gridSize/2f,Spline.InterpolationType.Qubic));
			}*/
			Update();
		}

		Vector2 getCenter(PointI point)
		{
			return (Vector2)point * gridSize + new Vector2(gridSize / 2, gridSize / 2);
		}

		public override void _Process(float delta)
		{
			if (pathUpdater.Pathversion != pathversion)
			{
				pathversion = pathUpdater.Pathversion;
				paths.Clear();
				splinePaths.Clear();
				foreach (PointI startP in start)
				{
					PointI[] path;
					pathUpdater.FindPath(out path, startP);
					paths.Add(path);
					splinePaths.Add(new SplinePath(path, gridSize, new Vector2(1, 1) * gridSize / 2f, InterpolationType.Linear));
				}
			}
		}
	}
}
