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

		string[] args = OS.GetCmdlineArgs();
		string loadingPath = "";
		foreach (string arg in args)
		{
			if (System.IO.File.Exists(arg) && arg.EndsWith(".xml"))
				loadingPath = arg;
		}

		if (loadingPath == "")
		{
			PackedScene pc = GD.Load<PackedScene>("res://Scenes/LevelLoader.tscn");
			Node nd = pc.Instance();
			SetScene(nd);
		}
		else
		{
			IPOW.IO.Loader.Load(loadingPath);
		}
	}

	public void SetScene(Node node)
	{
		ClearScene();
		attach.AddChild(node);
	}

	public void ClearScene()
	{
		while (attach.GetChildCount() > 0)
		{
			attach.RemoveChild(attach.GetChild(0));
		}
	}
}
