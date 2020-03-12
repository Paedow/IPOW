using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Pathing
{
    public class PathFinder
    {
        IGrid grid;
        public float[,] hops;   // Matrix of all distances to the nearest endpoint
        float maxHops;          // The maximum distance in the worst case
        int w, h;
        bool printDebug, printField;

        // List of all possible Jumps including diagonales
        Direction[] directionsDiag = new Direction[]{
            new Direction(1,0),
            new Direction(0,1),
            new Direction(-1,0),
            new Direction(0,-1),

            new Direction(1,1),
            new Direction(-1,1),
            new Direction(-1,-1),
            new Direction(1,-1)
        };

        public PathFinder(IGrid grid, bool printDebug = false, bool printField = false)
        {
            this.grid = grid;
            this.w = grid.GetGridWidth();
            this.h = grid.GetGridHeight();
            this.hops = new float[w, h];
            this.maxHops = w * h;
            this.printDebug = printDebug;
            this.printField = printField;
        }

        ///<summary>Sets the hopcount of the field [p_x,p_y] and all neighbours.</summary>
        void setHops(int p_x, int p_y, float hops)
        {
            // Cancel if field is outside grid
            if (p_x < 0 || p_x >= w || p_y < 0 || p_y >= h) return;
            // Cancel at walls
            if (grid.FieldBlocked(p_x, p_y)) return;
            // Check if current hops is already smaller than new hops
            if (this.hops[p_x, p_y] > hops)
            {
                this.hops[p_x, p_y] = hops;
                // Set hops for all neighbours
                setHops(p_x + 1, p_y, hops + 1);
                setHops(p_x, p_y + 1, hops + 1);
                setHops(p_x - 1, p_y, hops + 1);
                setHops(p_x, p_y - 1, hops + 1);
            }
        }

        ///<summary>Sets the hopcount of each field to the maximum amount.</summary>
        public void Reset()
        {
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    this.hops[x, y] = maxHops + 1;
                }
            }
        }

        ///<summary>Starts the calculation of the hopcount at the position "end"</summary>
        public void CalcHops(PointI end)
        {
            Stopwatch stp = new Stopwatch();
            if(printDebug) stp.Start();
            setHops(end.X, end.Y, 0);
            if(printDebug)
            {
                stp.Stop();
                GD.Print("Calculated hops in ", stp.ElapsedMilliseconds, "ms");
            }
            if(printField)
            {
                GD.Print("Field:");
                for(int y = 0; y < h; y++)
                {
                    StringBuilder line = new StringBuilder();
                    line.Append("|");
                    for(int x = 0; x < w; x++)
                    {
                        if(grid.FieldBlocked(x,y))
                            line.AppendFormat("###|");
                        else
                            line.AppendFormat("{0,3}|", this.hops[x,y]);
                    }
                    GD.Print(line.ToString());
                }
            }
        }

        ///<summary>Makes one step of the Path</summary>
        bool step(List<PointI> path, PointI point)
        {
            // If point is at an endpoint, return true
            if (this.hops[point.X, point.Y] == 0)
            {
                path.Add(point);
                return true;
            }

            // Search for the Direction with the smallest amount of hops
            float min_hops = this.hops[point.X, point.Y];
            int min_pos = -1;
            for (int i = 0; i < directionsDiag.Length; i++)
            {
                // Add each direction to the current position
                PointI next = point + directionsDiag[i];
                // Check if new position is outside the Grid
                if (next.X < 0 || next.X >= w || next.Y < 0 || next.Y >= h) continue;
                if (this.hops[next.X, next.Y] < min_hops
                    // Check if path would strip a corner
                    && this.hops[next.X, point.Y] < maxHops + 1
                    && this.hops[point.X, next.Y] < maxHops + 1)
                {
                    min_hops = this.hops[next.X, next.Y];
                    min_pos = i;
                }
            }

            // If there is no smaller path, there is no possible path.false
            if (min_pos < 0) return false;
            path.Add(point);
            return step(path, point + directionsDiag[min_pos]);
        }

        ///<ummary>Starts the search for the shortest path at the position "start"</summary>
        public bool FindPath(out PointI[] path, PointI start)
        {
            Stopwatch stp = new Stopwatch();
            if(printDebug) stp.Start();
            List<PointI> p = new List<PointI>();
            bool result = step(p, start);
            if (result) path = p.ToArray();
            else path = new PointI[0];
            if(printDebug)
            {
                stp.Stop();
                GD.Print("Found path in ", stp.ElapsedMilliseconds, "ms");
            }
            return result;
        }

        public SplinePath FindPath(PointI start)
        {
            PointI[] pts;
            bool result = FindPath(out pts, start);
            if (result) return null;
            return new SplinePath(pts, .5f, new Vector2(.25f, .25f), InterpolationType.Qubic);
        }
    }
}