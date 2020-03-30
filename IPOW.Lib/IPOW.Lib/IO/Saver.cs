using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IPOWLib.IO
{
    public class Saver
    {
        public static string SaveToString(WorldDescriptor world)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings setting = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n"
            };
            using (XmlWriter xml = XmlWriter.Create(sb,setting))
            {
                xml.WriteStartElement("World");
                xml.WriteAttributeString("w", "" + world.Width);
                xml.WriteAttributeString("h", "" + world.Height);

                string baseTile = "FlatTile";
                xml.WriteStartElement("BaseTile");
                xml.WriteAttributeString("value", baseTile);
                xml.WriteEndElement();

                for(int y = 0; y < world.Height;y++)
                {
                    for(int x = 0; x < world.Width;x++)
                    {
                        string tile = world.Tiles[x, y].TypeName;
                        if(tile != baseTile)
                        {
                            xml.WriteStartElement("Tile");
                            xml.WriteAttributeString("x", "" + x);
                            xml.WriteAttributeString("y", "" + y);
                            xml.WriteAttributeString("type", tile);
                            xml.WriteEndElement();
                        }
                    }
                }

                for(int i = 0; i < world.Waves.Length;i++)
                {
                    xml.WriteStartElement("Wave");
                    xml.WriteAttributeString("ticks", "" + world.Waves[i].TicksToSpawn);
                    for(int b = 0; b < world.Waves[i].Enemies.Length; b++)
                    {
                        xml.WriteStartElement("Enemy");
                        xml.WriteAttributeString("type", world.Waves[i].Enemies[b]);
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
            }

            return sb.ToString();
        }
    }
}
