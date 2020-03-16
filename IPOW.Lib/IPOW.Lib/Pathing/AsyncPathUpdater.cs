using Godot;
using System;
using Thread = System.Threading.Thread;

namespace IPOWLib.Pathing
{
    public class AsyncPathUpdater
    {
        Thread thread = null;
        IGrid grid;
        Grid gridCopy;
        PathFinder pathFinder;
        SplinePath path;
        PointI[] endPoints;
        public uint Pathversion { get; private set; } = 0;
        public PathFinder PathFinder
        {
            get
            {
                return pathFinder;
            }
        }

        public AsyncPathUpdater(IGrid grid)
        {
            this.grid = grid;
            this.path = new SplinePath(new Vector2[0], InterpolationType.Null);
            this.pathFinder = new PathFinder(grid);
        }

        public void Update(PointI[] endPoints)
        {
            thread?.Abort();
            this.endPoints = endPoints;
            gridCopy = new Grid();
            gridCopy.Copy(this.grid);
            thread = new Thread(loop);
            thread.Start();
        }

        public void Update(IGrid grid, PointI[] endPoints)
        {
            this.grid = grid;
            Update(endPoints);
        }

        void loop()
        {
            try
            {
                PointI[] tmp = new PointI[endPoints.Length];
                endPoints.CopyTo(tmp, 0);
                this.pathFinder.Reset(gridCopy);
                foreach (PointI end in tmp)
                    this.pathFinder.CalcHops(end);
                Pathversion++;
            }
            catch (System.Threading.ThreadAbortException) { }
        }

        public bool FindPath(out PointI[] path, PointI start)
        {
            return pathFinder.FindPath(out path, start);
        }
    }
}