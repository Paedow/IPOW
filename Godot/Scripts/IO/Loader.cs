using Godot;
using System;
using IPOWLib.IO;
using IPOW.Tiles;
using System.Collections.Generic;

namespace IPOW.IO
{
    public class Loader
    {
        static Dictionary<string, PackedScene> scenes;
        static bool initialized = false;

        public static World LoadWorld( WorldDescriptor worldDescriptor)
        {
            World world = (World)GD.Load<PackedScene>("res://Scenes/World.tscn").Instance();
            Grid3D grid = new Grid3D(world, worldDescriptor.Width, worldDescriptor.Height);
            world.AddChild(grid);

            for(int y = 0; y < worldDescriptor.Height; y++)
            {
                for(int x = 0; x < worldDescriptor.Width; x++)
                {
                    TileDescriptor tileD = worldDescriptor.Tiles[x,y];
                    Tile tileT = CreateTile(tileD);
                    grid.SetTile(tileT,x,y,false);
                }
            }

            return world;
        }

        public static Tile CreateTile(TileDescriptor tileDescriptor)
        {
            initTiles();
            Tile t = (Tile)scenes[tileDescriptor.TypeName].Instance();
            return t;
        }

        static void initTiles()
        {
            if(initialized) return;
            initialized = true;
            scenes = new Dictionary<string, PackedScene>();
            addScene("FlatTile", "res://Scenes/Tiles/FlatTile.tscn");
            addScene("Hill", "res://Scenes/Tiles/Hill.tscn");
            addScene("Spawner", "res://Scenes/Tiles/Spawner.tscn");
        }

        static void addScene(string name, string path)
        {
            PackedScene scene = GD.Load<PackedScene>(path);
            scenes.Add(name, scene);
        }

        public static void Load(string path)
        {
            GD.Print("Loading Level: '", path, "'");
			string text = System.IO.File.ReadAllText(path);
			WorldDescriptor wd = IPOWLib.IO.Loader.Load(text);
			World w = Loader.LoadWorld(wd);
			RootNode.GetNode().SetScene(w);
        }
    }
}