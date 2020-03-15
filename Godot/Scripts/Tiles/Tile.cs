using System;
using Godot;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
    public class Tile : Spatial
    {
        public MovementLayer BlockedLayer { get; protected set; } = 0;
        public Grid3D ParentGrid { get; protected set; } = null;
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;

        public virtual void SetPosition(Grid3D parent, int x, int y)
        {
            this.ParentGrid = parent;
            float scale = parent.GetGridSize();
            Vector3 pos = new Vector3(scale * x, 0, scale * y);
            this.Translation = pos;
        }

        public virtual bool IsBlocked(MovementLayer layer)
        {
            return ((BlockedLayer & layer) != 0) ;
        }
    }
}
