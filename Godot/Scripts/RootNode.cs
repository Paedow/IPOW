using Godot;
using System;

public class RootNode : Spatial
{
    static RootNode rootNode = null;

    Spatial attach;

    public RootNode()
    {
        rootNode = this;
    }

    public static RootNode GetNode()
    {
        return rootNode;
    }

    public override void _Ready()
    {
        attach = (Spatial)GetNode(new NodePath("Attach"));

        PackedScene pc = GD.Load<PackedScene>("res://Scenes/LevelLoader.tscn");
        Node nd = pc.Instance();
        SetScene(nd);
    }

    public void SetScene(Node node)
    {
        ClearScene();
        attach.AddChild(node);
    }

    public void ClearScene()
    {
        while(attach.GetChildCount() > 0)
        {
            attach.RemoveChild(attach.GetChild(0));
        }
    }
}
