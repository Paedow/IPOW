using Godot;
using System;
using System.Xml;
using System.IO;

namespace IPOWLib.IO
{
    public class Loader
    {
        public static WorldDescriptor Load(string text)
        {
            WorldDescriptor world = new WorldDescriptor();
            StringReader str = new StringReader(text);
            using(XmlReader xml = XmlReader.Create(str))
            {
                while(xml.Read())
                {
                    switch(xml.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                if(xml.Name == "BaseTile")
                                {
                                    string tileName = xml.GetAttribute("value");
                                    TileDescriptor tile = TileDescriptor.Create(tileName);
                                    fillGrid(world, tile);
                                }
                                else if(xml.Name == "World")
                                {
                                    string sw = xml.GetAttribute("w");
                                    string sh = xml.GetAttribute("h");
                                    int w, h;
                                    int.TryParse(sw, out w);
                                    int.TryParse(sh, out h);
                                    world.Width = w;
                                    world.Height = h;
                                    world.Tiles = new TileDescriptor[w, h];
                                }
                                else if(xml.Name == "Tile")
                                {
                                    string sx = xml.GetAttribute("x");
                                    string sy = xml.GetAttribute("y");
                                    string type = xml.GetAttribute("type");

                                    int x, y;
                                    int.TryParse(sx, out x);
                                    int.TryParse(sy, out y);
                                    TileDescriptor tile = TileDescriptor.Create(type);
                                    world.Tiles[x, y] = tile;
                                }
                            }
                            break;
                    }
                }
            }
            return world;
        }

        static void fillGrid(WorldDescriptor world, TileDescriptor tile)
        {
            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    world.Tiles[x, y] = tile;
                }
            }
        }
    }
}