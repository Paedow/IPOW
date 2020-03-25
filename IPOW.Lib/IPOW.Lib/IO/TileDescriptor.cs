using Godot;
using System;

namespace IPOWLib.IO
{
    public struct TileDescriptor
    {
        public string TypeName;

        public static TileDescriptor Create(string name)
        {
            TileDescriptor tile = new TileDescriptor();
            tile.TypeName = name;
            return tile;
        }
    }
}