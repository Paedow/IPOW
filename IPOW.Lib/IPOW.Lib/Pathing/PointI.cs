using Godot;
using System;

namespace Pathing
{
    public class PointI
    {
        public int X;
        public int Y;

        public PointI(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static explicit operator Vector2(PointI point)
        {
            return new Vector2(point.X, point.Y);
        }

        public PointI Add(int x, int y)
        {
            return new PointI(this.X + x, this.Y + y);
        }

        public static PointI operator +(PointI a, PointI b)
        {
            return new PointI(a.X + b.X, a.Y + b.Y);
        }

        public static PointI operator +(PointI a, Direction b)
        {
            return new PointI(a.X + b.X, a.Y + b.Y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PointI))
                return false;

            PointI point = (PointI)obj;
            return point.X == this.X && point.Y == this.Y;
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}