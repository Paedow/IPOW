using Godot;
using System;
using Mouse3D;

namespace IPOW.Tiles
{
    public class Grid3D : Spatial
    {
        Camera camera;
        int lastButtonMask = 0;

        public int Width { get; private set; } = 20;
        public int Height { get; private set; } = 20;

        public Spatial[,] Grid { get; private set; }

        PackedScene sceneTile;
        PackedScene sceneTower;
        Spatial glowTile;

        public override void _Ready()
        {
            camera = GetNode<Camera>(new NodePath("../CameraRig/Camera"));
            this.Grid = new Spatial[Width, Height];

            sceneTile = GD.Load<PackedScene>("res://Scenes/Tiles/FlatTile.tscn");
            sceneTower = GD.Load<PackedScene>("res://Scenes/Tiles/TestTower.tscn");
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Spatial tile = (Spatial)sceneTile.Instance();
                    SetTile(tile, x, y);
                }

            var sceneGlowTile = GD.Load<PackedScene>("res://Scenes/Objects/TileGlow.tscn");
            glowTile = (Spatial)sceneGlowTile.Instance();
            AddChild(glowTile);
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouse)
            {
                InputEventMouse mouseEvent = (InputEventMouse)@event;
                var ray = new MouseRay(camera, mouseEvent.GlobalPosition);
                Vector3? pos = ray.PositionOnPlane(new Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1)));
                if (mouseEvent.ButtonMask == 1 && lastButtonMask == 0)
                {
                    if (pos.HasValue && pos.Value.x > 0 && pos.Value.z > 0)
                    {
                        Vector2 gPos = new Vector2(pos.Value.x, pos.Value.z);
                        gPos.x = (int)(gPos.x * 2);
                        gPos.y = (int)(gPos.y * 2);
                        GD.Print(gPos);
                        Spatial tile = (Spatial)sceneTower.Instance();
                        SetTile(tile, (int)gPos.x, (int)gPos.y);
                    }
                }

                if (pos.HasValue)
                {
                    Vector3 iPos = new Vector3(
                        ((int)(pos.Value.x * 2) / 2f),
                        ((int)(pos.Value.y * 2) / 2f),
                        ((int)(pos.Value.z * 2) / 2f));
                    glowTile.Translation = iPos;
                    if(iPos.x < 0 || iPos.z < 0 || iPos.x*2 >= Width||iPos.z*2 >= Height)
                    {
                        glowTile.Visible = false;
                    }
                    else
                    {
                        glowTile.Visible = true;
                    }
                }

                lastButtonMask = mouseEvent.ButtonMask;
            }
        }

        public void SetTile(Spatial tile, int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return;
            if (this.Grid[x, y] != null)
            {
                this.RemoveChild(this.Grid[x, y]);
                this.Grid[x, y].Dispose();
            }
            if (tile is Tile)
            {
                ((Tile)tile).SetPosition(this, x, y);
            }
            else
            {
                tile.Translation = new Vector3(x / 2f, 0, y / 2f);
            }
            this.Grid[x, y] = tile;
            this.AddChild(tile);
        }

        public float GetGridSize()
        {
            return 0.5f;
        }
    }
}