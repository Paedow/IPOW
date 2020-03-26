using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPOW.Editor.Tiles;
using IPOWLib.IO;

namespace IPOW.Editor
{
    public class World
    {
        public Tile[,] Grid;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public World(int w, int h)
        {
            this.Width = w;
            this.Height = h;

            this.Grid = new Tile[w, h];

            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    Tile t = new FlatTile();
                    t.SetPos(x, y);
                    Grid[x, y] = t;
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.Grid[x, y].Draw();
                }
            }
        }

        public WorldDescriptor GetDescriptor()
        {
            WorldDescriptor wd = new WorldDescriptor();
            wd.Width = this.Width;
            wd.Height = this.Height;
            wd.Tiles = new TileDescriptor[Width, Height];
            for(int x = 0;x<Width;x++)
            {
                for(int y = 0; y < Height;y++)
                {
                    string tName = Grid[x, y].Name;
                    TileDescriptor td = TileDescriptor.Create(tName);
                    wd.Tiles[x, y] = td;
                }
            }
            return wd;
        }

        public static World FromDescriptor(WorldDescriptor wd)
        {
            World w = new World(wd.Width, wd.Height);
            for (int x = 0; x < wd.Width; x++)
            {
                for (int y = 0; y < wd.Height; y++)
                {
                    string tName = wd.Tiles[x, y].TypeName;
                    Tile tile = Tile.GetTile(tName);
                    tile?.SetPos(x, y);
                    if (tile != null) w.Grid[x, y] = tile;
                }
            }
            return w;
        }
    }
}
