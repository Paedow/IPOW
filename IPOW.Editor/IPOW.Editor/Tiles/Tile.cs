using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOW.Editor.Tiles
{
    public abstract class Tile
    {
        public Texture Image { get; protected set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public void SetPos(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public virtual void Draw()
        {
            Renderer.DrawImage(new System.Drawing.RectangleF(X, Y, 32, 32), Image);
        }
    }
}
