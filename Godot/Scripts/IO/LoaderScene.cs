using Godot;
using System;
using IPOWLib.IO;

namespace IPOW.IO
{
	public class LoaderScene : Spatial
	{
		FileDialog fd;

		public override void _Ready()
		{
			/*string text = System.IO.File.ReadAllText(@"E:\Entwicklung\Godot\IPOW\IPOW\Levels\base.xml");
			WorldDescriptor wd = IPOWLib.IO.Loader.Load(text);
			World w = Loader.LoadWorld(wd);
			AddChild(w);*/

			fd = (FileDialog)GetNode(new NodePath("FileDialog"));
			fd.ToSignal(fd, "file_selected").OnCompleted(()=>{
				Load(fd.CurrentPath);
			});
			fd.Show();
		}

		public void Load(string path)
		{
			GD.Print("Loading Level: '", path, "'");
			string text = System.IO.File.ReadAllText(path);
			WorldDescriptor wd = IPOWLib.IO.Loader.Load(text);
			World w = Loader.LoadWorld(wd);
			AddChild(w);
		}
	}
}
