using System;
using Godot;
using IPOWLib.Pathing;

namespace IPOW.Tiles
{
    public class Tile : Spatial
    {
        public const string SHADER_PATH = "res://Shader/TowerShader.shader";
        public MovementLayer BlockedLayer { get; protected set; } = 0;
        public Grid3D ParentGrid { get; protected set; } = null;
        public bool CanPlaceOn { get; protected set; } = false;
        public int X { get; private set; } = 0;
        public int Y { get; private set; } = 0;
        public Tile LastTile { get; set; } = null;
        public TileState State { get; set; } = TileState.Running;
        TileState _state = TileState.Null;

        public override void _Ready()
        {
            setShaderMode(this,true,true);
        }

        public override void _Process(float delta)
        {
            if(State != _state)
            {
                switch(State)
                {
                    case TileState.Highlight: setShaderMode(this,true,true);
                    break;
                    case TileState.Place: setShaderMode(this,false,true);
                    break;
                    case TileState.Running: setShaderMode(this,true,false);
                    break;
                }
                _state = State;
            }

            switch(State)
            {
                case TileState.Highlight:
                case TileState.Running:
                {
                    TileProcess(delta);
                }
                break;
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            switch(State)
            {
                case TileState.Highlight:
                case TileState.Running:
                {
                    TilePhysicsProcess(delta);
                }
                break;
            }
        }

        public virtual void SetPosition(Grid3D parent, int x, int y)
        {
            this.ParentGrid = parent;
            float scale = parent.GetGridSize();
            Vector3 pos = new Vector3(scale * x, 0, scale * y);
            this.Translation = pos;
            this.X = x;
            this.Y = y;
        }

        public virtual bool IsBlocked(MovementLayer layer)
        {
            return ((BlockedLayer & layer) != 0);
        }

        public virtual void GridReady(Grid3D parent, int x, int y)
        {

        }

        public virtual string[] GetCommands()
        {
            return new string[0];
        }

        public virtual void RunCommand(string cmd)
        {

        }

        public virtual void TileProcess(float delta)
        {

        }

        public virtual void TilePhysicsProcess(float delta)
        {

        }

        void setShaderMode(Node node, bool showTexture, bool highlight)
        {
            if(node is MeshInstance)
            {
                MeshInstance mesh = (MeshInstance)node;
                if(mesh.GetSurfaceMaterial(0) is ShaderMaterial)
                {
                    ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceMaterial(0);
                    if(mat.Shader.ResourcePath == SHADER_PATH)
                    {
                        ShaderMaterial nmat = (ShaderMaterial)mat.Duplicate();

                        nmat.SetShaderParam("ShowTexture", showTexture);
                        nmat.SetShaderParam("Highlight", highlight);
                        nmat.SetShaderParam("PointSize", 2);

                        mesh.SetSurfaceMaterial(0,nmat);

                        mat.Dispose();
                    }
                }
            }
            for(int i = 0; i < node.GetChildCount(); i++)
            {
                setShaderMode(node.GetChild(i), showTexture, highlight);
            }
        }

        public virtual Color GetMinimapColor()
        {
            return MinimapColors.NONE;
        }

        public enum TileState { Null, Place, Running, Highlight };
    }
}
