using Godot;
using System;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
    public class SurroundingTilemap
    {
        bool right, left, top, bottom;

        public SurroundingTilemap(bool top, bool left, bool right, bool bottom)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.bottom = bottom;
        }

        public virtual bool MatchPattern(Grid3D grid, int x, int y, params Type[] types)
        {
            return isType(grid, x, y - 1, types) == top
                && isType(grid, x - 1, y, types) == left
                && isType(grid, x + 1, y, types) == right
                && isType(grid, x, y + 1, types) == bottom;
        }

        protected bool isType(Grid3D grid, int x, int y, Type[] types)
        {
            if (x < 0 || y < 0 || x >= grid.Width || y >= grid.Height)
                return false;
            foreach (Type t in types)
                if (t.IsInstanceOfType(grid.Grid[x, y]))
                    return true;
            return false;
        }
    }

    public class SurroundingTilemapDiag : SurroundingTilemap
    {
        bool tl, tr, bl, br;

        public SurroundingTilemapDiag(bool tl, bool tr, bool bl, bool br)
            : base(true, true, true, true)
        {
            this.tl = tl;
            this.tr = tr;
            this.bl = bl;
            this.br = br;
        }

        public override bool MatchPattern(Grid3D grid, int x, int y, params Type[] types)
        {
            return isType(grid, x - 1, y - 1, types) == tl
                && isType(grid, x - 1, y + 1, types) == tr
                && isType(grid, x + 1, y - 1, types) == bl
                && isType(grid, x + 1, y + 1, types) == br;
        }
    }
}