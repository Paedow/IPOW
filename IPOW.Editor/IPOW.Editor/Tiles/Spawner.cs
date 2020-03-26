using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOW.Editor.Tiles
{
    [TN("Spawner")]
    public class Spawner : Tile
    {
        public override void Draw()
        {
            var c = System.Drawing.Color.Cyan;
            Renderer.FillRect(new System.Drawing.RectangleF(X * 32, Y * 32, 32, 32), c);
        }
    }
}
