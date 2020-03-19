using System;

namespace IPOWLib.Pathing
{
    public interface IGrid
    {
        bool FieldBlocked(int x, int y);
        int GetGridWidth();
        int GetGridHeight();
    }
}