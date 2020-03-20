using System;
using Godot;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
    public class Tile : Spatial
    {
        public MovementLayer BlockedLayer { get; protected set; } = 0;
        public Grid3D ParentGrid { get; protected set; } = null;
        public bool CanPlaceOn{get;protected set;} = false;
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;
        public Tile LastTile{get;set;} = null;

        public virtual void SetPosition(Grid3D parent, int x, int y)
        {
            this.ParentGrid = parent;
            float scale = parent.GetGridSize();
            Vector3 pos = new Vector3(scale * x, 0, scale * y);
            this.Translation = pos;
            this.X = x;
            this.Y = y;
        }

        public virtual bool IsBlocked(MovementLayer layer)
        {
            return ((BlockedLayer & layer) != 0) ;
        }

        public virtual void GridReady(Grid3D parent, int x, int y)
        {

        }

        public virtual string[] GetCommands()
        {
            return new string[0];
        }

        public virtual void RunCommand(string cmd)
        {
        
        }
    }
}
