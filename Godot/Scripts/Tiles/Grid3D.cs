using Godot;
using System;
using Mouse3D;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
    public class Grid3D : Spatial
    {
        Camera camera;
        int lastButtonMask = 0;

        public int Width { get; private set; } = 40;
        public int Height { get; private set; } = 20;

        public Tile[,] Grid { get; private set; }

        PackedScene sceneTile, sceneTower, sceneHill;
        Spatial glowTile;
        Spatial walls;
        PointI[] endPoints;
        uint pathversion = 0;
        World world;

        AsyncPathUpdater pathUpdaterGround;

        public Grid3D(World world)
        {
            this.world = world;
        }

        public override void _Ready()
        {
            camera = GetNode<Camera>(new NodePath("../CameraRig/Camera"));
            this.Grid = new Tile[Width, Height];

            sceneTile = GD.Load<PackedScene>("res://Scenes/Tiles/FlatTile.tscn");
            sceneTower = GD.Load<PackedScene>("res://Scenes/Tiles/TestTower.tscn");
            sceneHill = GD.Load<PackedScene>("res://Scenes/Tiles/Hill.tscn");
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    Tile tile = (Tile)sceneTile.Instance();
                    SetTile(tile, x, y, false);
                }

            for (int x = 5; x < Width - 10; x++)
            {
                for (int y = 0; y < Height / 2 - 2; y++)
                {
                    Tile tile = (Tile)sceneHill.Instance();
                    SetTile(tile, x, y, false);
                    tile = (Tile)sceneHill.Instance();
                    SetTile(tile, x, Height - y, false);
                }
            }
            GridReady();

            var sceneGlowTile = GD.Load<PackedScene>("res://Scenes/Objects/TileGlow.tscn");
            glowTile = (Spatial)sceneGlowTile.Instance();
            AddChild(glowTile);

            walls = GetNode<Spatial>("../Walls");
            walls.Scale = new Vector3(Width, 1, Height);

            endPoints = new PointI[] { new PointI(Width - 2, Height / 2) };
            pathUpdaterGround = new AsyncPathUpdater(GetGrid(MovementLayer.Ground));
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
                        gPos.x = (int)(gPos.x);
                        gPos.y = (int)(gPos.y);
                        GD.Print(gPos);
                        Tile tile = (Tile)sceneTower.Instance();
                        SetTile(tile, (int)gPos.x, (int)gPos.y);
                        pathUpdaterGround.Update(GetGrid(MovementLayer.Ground), endPoints);
                    }
                }

                if (pos.HasValue)
                {
                    Vector3 iPos = new Vector3(
                        ((int)pos.Value.x),
                        ((int)pos.Value.y),
                        ((int)pos.Value.z));
                    glowTile.Translation = iPos;
                    if (iPos.x < 0 || iPos.z < 0 || iPos.x >= Width || iPos.z >= Height)
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

        public override void _Process(float delta)
        {
            if (pathUpdaterGround != null && pathUpdaterGround.Pathversion != pathversion)
            {
                pathversion = pathUpdaterGround.Pathversion;
                world.UpdatePath(pathUpdaterGround.PathFinder);
            }
        }

        public void SetTile(Tile tile, int x, int y, bool update = true)
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
            if (update)
            {
                world.GridChanged();
                GridReady();
            }
        }

        public float GetGridSize()
        {
            return 1;
        }

        public Grid GetGrid(MovementLayer layer)
        {
            Grid g = new Grid(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    g.SetField(x, y, Grid[x, y].IsBlocked(layer));
                }
            }
            return g;
        }

        public void GridReady()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Grid[x, y].GridReady(this, x, y);
                }
            }
        }
    }
}