using Godot;
using System;

namespace IPOW.Util
{
    public class EditTool
    {
        public enum Tool
        {
            None, PlaceTower
        }

        public static Tool EditingTool { get; set; } = Tool.None;
        public static PackedScene SelectedTower { get; set; } = null;
    }
}