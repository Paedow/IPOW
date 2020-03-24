using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPOW.Editor.Tiles;

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
    }
}
