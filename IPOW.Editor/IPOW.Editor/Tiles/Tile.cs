using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IPOW.Editor.Tiles
{
    public abstract class Tile
    {
        public static Dictionary<string, Type> Tiles { get; private set; }

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

        public static void Init()
        {
            Tiles = new Dictionary<string, Type>();
            foreach(Type type in Assembly.GetCallingAssembly().GetTypes())
            {
                if(type.IsSubclassOf(typeof(Tile)))
                {
                    TN[] attributes = type.GetCustomAttributes<TN>().ToArray();
                    if(attributes.Length > 0)
                    {
                        string name = attributes[0].Name;
                        Tiles.Add(name, type);
                    }
                }
            }
        }
    }

    public class TN : Attribute
    {
        public string Name;

        public TN(string name)
        {
            this.Name = name;
        }
    }
}
