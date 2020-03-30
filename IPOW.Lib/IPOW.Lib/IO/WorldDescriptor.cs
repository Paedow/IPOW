using Godot;
using System;

namespace IPOWLib.IO
{
    public class WorldDescriptor
    {
        public int Width;
        public int Height;
        public TileDescriptor[,] Tiles;
        public WaveDescriptor[] Waves;

        public WorldDescriptor()
        {
            Waves = new WaveDescriptor[0];
        }
    }
}