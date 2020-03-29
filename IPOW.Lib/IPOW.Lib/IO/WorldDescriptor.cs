using Godot;
using System;

namespace IPOWLib.IO
{
    public struct WorldDescriptor
    {
        public int Width;
        public int Height;
        public TileDescriptor[,] Tiles;
    }
}