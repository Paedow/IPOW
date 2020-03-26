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
        public string Name { get; private set; } = "null";

        public Tile()
        {
            TN[] tns = this.GetType().GetCustomAttributes<TN>().ToArray();
            if (tns.Length > 0) Name = tns[0].Name;
        }

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

        public static Tile GetTile(string name)
        {
            if (!Tiles.ContainsKey(name)) return null;
            Type type = Tiles[name];
            ConstructorInfo constructor = type.GetConstructor(new Type[0]);
            Tile tile = (Tile)constructor.Invoke(new object[0]);
            return tile;
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
