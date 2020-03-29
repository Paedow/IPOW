using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOW.Editor.Tiles
{
    [TN("Hill")]
    public class Hill : Tile
    {
        public override void Draw()
        {
            var c = System.Drawing.Color.DarkGreen;
            Renderer.FillRect(new System.Drawing.RectangleF(X * 32, Y * 32, 32, 32), c);
        }
    }
}
