using Godot;
using System;
using Thread = System.Threading.Thread;

namespace Pathing
{
    public class AsyncPathUpdater
    {
        bool running, update;
        Thread thread = null;
        IGrid grid;
        PathFinder pathFinder;
        SplinePath path;
        PointI[] endPoints;
        public uint Pathversion{get; private set;} = 0;

        public AsyncPathUpdater(IGrid grid)
        {
            this.grid = grid;
            this.running = false;
            this.update = false;
            this.path = new SplinePath(new Vector2[0], InterpolationType.Null);
            this.pathFinder = new PathFinder(grid);
        }

        public void Start()
        {
            thread = new Thread(loop);
            this.running = true;
            thread.Start();
        }

        public void Stop()
        {
            thread?.Abort();
            thread = null;
            this.running = false;
        }

        public void Update(PointI[] endPoints)
        {
            this.endPoints = endPoints;
            update = true;
            thread.Abort();
            thread.Join();
            thread = new Thread(loop);
            thread.Start();
        }

        void loop()
        {
            try
            {
                while (running)
                {
                    if(update)
                    {
                        update = false;
                        PointI[] tmp = new PointI[endPoints.Length];
                        endPoints.CopyTo(tmp, 0);
                        Grid cg = new Grid();
                        cg.Copy(grid);
                        this.pathFinder.Reset(cg);
                        foreach(PointI end in tmp)
                            this.pathFinder.CalcHops(end);
                        Pathversion++;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                
            }
        }

        public bool FindPath(out PointI[] path, PointI start)
        {
            return pathFinder.FindPath(out path, start);
        }
    }
}