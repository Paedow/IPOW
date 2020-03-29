using Godot;
using System;
using System.Collections.Generic;

namespace IPOW.Util
{
    public static class GUITools
    {
        public static List<Control> GetAllControls(this Node parent)
        {
            List<Control> panels = new List<Control>();
            for (int i = 0; i < parent.GetChildCount(); i++)
            {
                Node child = parent.GetChild(i);
                if (child is Control)
                {
                    panels.Add((Control)child);
                }
                panels.AddRange(GetAllControls(child));
            }
            return panels;
        }

        public static bool IsPointOnGUI(this Node parent, Vector2 point)
        {
            List<Control> panels = GetAllControls(parent);
            foreach (Control panel in panels)
            {
                Rect2 rect = new Rect2(panel.RectGlobalPosition, panel.RectSize);
                if (rect.HasPoint(point)) return true;
            }
            return false;
        }
    }
}