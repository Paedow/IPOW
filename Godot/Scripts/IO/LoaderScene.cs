using Godot;
using System;
using IPOWLib.IO;

namespace IPOW.IO
{
	public class LoaderScene : Spatial
	{
		public override void _Ready()
		{
			string text = System.IO.File.ReadAllText(@"E:\Entwicklung\Godot\IPOW\IPOW\Levels\base.xml");
			WorldDescriptor wd = IPOWLib.IO.Loader.Load(text);
			World w = Loader.LoadWorld(wd);
			AddChild(w);
		}
	}
}
