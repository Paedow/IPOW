using Godot;
using System;
using System.Text;

namespace Pathing
{
    public class Grid : IGrid
    {
        int w, h;
        bool[,] buffer;

        public Grid()
        {

        }

        public Grid(int w, int h)
        {
            this.w = w;
            this.h = h;
            this.buffer = new bool[w, h];
        }

        public void Copy(IGrid grid)
        {
            w = grid.GetGridWidth();
            h = grid.GetGridHeight();

            buffer = new bool[w, h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    buffer[x, y] = grid.FieldBlocked(x, y);
                }
            }
        }

        public int GetGridWidth()
        {
            return w;
        }

        public int GetGridHeight()
        {
            return h;
        }

        public bool FieldBlocked(int x, int y)
        {
            return buffer[x, y];
        }

        public void SetField(int x, int y, bool blocked)
        {
            buffer[x, y] = blocked;
        }

        bool hasPoint(PointI[] array, int x, int y)
        {
            foreach (var p in array)
                if (p.X == x && p.Y == y)
                    return true;
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < h; y++)
            {
                sb.Append("|");
                for (int x = 0; x < w; x++)
                {
                    sb.Append(buffer[x, y] ? '#' : '.');
                }
                sb.AppendLine("|");
            }
            return sb.ToString();
        }

        public string ToString(PointI[] path)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < h; y++)
            {
                sb.Append("|");
                for (int x = 0; x < w; x++)
                {
                    bool wall = buffer[x, y];
                    bool onPath = hasPoint(path, x, y);
                    char chr = '.';
                    if (wall && onPath) chr = '*';
                    else if (wall) chr = '#';
                    else if (onPath) chr = '+';
                    sb.Append(chr);
                }
                sb.AppendLine("|");
            }
            return sb.ToString();
        }
    }
}