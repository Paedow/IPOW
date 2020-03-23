using Godot;
using System;
using IPOW.Tiles;

namespace IPOW.GUI
{
    public class Minimap : Panel
    {
        Grid3D grid;
        World world;

        public override void _Ready()
        {
            world = (World)GetNode("../..");
            grid = world.Grid;
        }

        public override void _Draw()
        {
            if (world.Grid != null)
            {
                Rect2 scale = world.Grid.DrawMinimap(this, new Rect2(0, 0, this.RectSize.x, this.RectSize.y));
                for (int i = 0; i < world.Creeps.Count; i++)
                {
                    Vector3 p3 = world.Creeps[i].Translation;
                    Vector2 p = new Vector2(p3.x, p3.z) * scale.Size + scale.Position;
                    Rect2 r = new Rect2(p.x - 2, p.y - 2, 4, 4);
                    DrawRect(r, MinimapColors.CREEP);
                }
            }

        }

        public override void _Process(float delta)
        {
            Update();
        }
    }
}