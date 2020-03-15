using Godot;
using System;

namespace IPOWLib.Pathing
{
public class Direction
{
    public int X, Y;
    public float L;

    public Direction(int x, int y)
    {
        this.X = x;
        this.Y = y;
        this.L = Mathf.Sqrt(x * x + y * y);
    }
}
}