using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Reflection;

namespace IPOW.Editor
{
    public partial class LevelControl : UserControl
    {
        GLControl control;
        bool devenv;
        int levelW, levelH;
        public World World { get; private set; }
        int scrollBarFactor = 5;
        bool leftDown = false;
        Point pos1;
        Point pos2;
        Type type = null;


        public LevelControl()
        {
            InitializeComponent();

            devenv = Process.GetCurrentProcess().ProcessName == "devenv";

            control = new GLControl();
            control.Dock = DockStyle.Fill;

            if(!devenv)
                container.Controls.Add(control);

            if (!devenv)
                control.Paint += Control_Paint;

            this.Resize += LevelControl_Resize;
            xScroll.Scroll += XScroll_Scroll;
            yScroll.Scroll += YScroll_Scroll;

            World = new World(25, 15);
            SetSize();

            control.MouseDown += LevelControl_MouseDown;
            control.MouseUp += LevelControl_MouseUp;
            control.MouseMove += LevelControl_MouseMove;
        }

        private void LevelControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = getPos(e.Location);
            if (leftDown)
            {
                pos2 = pos;
                leftDown = true;
                control.Invalidate();
            }
        }

        private void LevelControl_MouseUp(object sender, MouseEventArgs e)
        {
            Point pos = getPos(e.Location);
            if (leftDown)
            {
                pos2 = pos;
                leftDown = false;

                if(type != null && type.IsSubclassOf(typeof(Tiles.Tile)))
                {
                    Rectangle rect = getRect(pos1, pos2, 1);
                    ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                    for(int x = rect.X;x<rect.Right;x++)
                    {
                        for(int y = rect.Y;y<rect.Bottom;y++)
                        {
                            if (x < 0 || x >= World.Width || y < 0 || y >= World.Height) continue;
                            Tiles.Tile tile = (Tiles.Tile)constructor.Invoke(new object[0]);
                            tile.SetPos(x, y);
                            World.Grid[x, y] = tile;
                        }
                    }
                }

                control.Invalidate();
            }
        }

        private void LevelControl_MouseDown(object sender, MouseEventArgs e)
        {
            Point pos = getPos(e.Location);
            if(e.Button == MouseButtons.Left)
            {
                pos1 = pos2 = pos;
                leftDown = true;
                control.Invalidate();
            }
        }

        Point getPos(Point mPos)
        {
            Point offset = getOffset();
            Point pos = new Point(mPos.X + offset.X, mPos.Y + offset.Y);
            pos.X /= 32;
            pos.Y /= 32;
            return pos;
        }

        Point getOffset()
        {
            int offsetX = xScroll.Value * scrollBarFactor - 64;
            int offsetY = yScroll.Value * scrollBarFactor - 64;
            return new Point(offsetX, offsetY);
        }

        Rectangle getRect(Point p1, Point p2, int factor = 1)
        {
            int p1x = Math.Min(p1.X, p2.X) * factor;
            int p2x = Math.Max(p1.X, p2.X) * factor;
            int p1y = Math.Min(p1.Y, p2.Y) * factor;
            int p2y = Math.Max(p1.Y, p2.Y) * factor;
            return new Rectangle(p1x, p1y, p2x - p1x + factor, p2y - p1y + factor);
        }

        private void YScroll_Scroll(object sender, ScrollEventArgs e)
        {
            control.Invalidate();
        }

        private void XScroll_Scroll(object sender, ScrollEventArgs e)
        {
            control.Invalidate();
        }

        private void LevelControl_Resize(object sender, EventArgs e)
        {
            SetSize();
            control.Invalidate();
        }

        public void SetSize()
        {
            this.levelW = World.Width * 32;
            this.levelH = World.Height * 32;

            int scrollMaxY = levelH - control.ClientSize.Height + 128;
            int scrollMaxX = levelW - control.ClientSize.Width + 128;

            if (scrollMaxX > 0)
                xScroll.Maximum = scrollMaxX / scrollBarFactor;
            xScroll.Enabled = scrollMaxX > 0;
            if (scrollMaxY > 0)
                yScroll.Maximum = scrollMaxY / scrollBarFactor;
            yScroll.Enabled = scrollMaxY > 0;
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            if (devenv) return;
            control.MakeCurrent();

            GL.Enable(EnableCap.Blend);
            Renderer.SetBlendFunc();
            Renderer.Clear(SystemColors.ControlDark);
            Renderer.SetViewport(control.Width, control.Height);

            var offset = getOffset();
            GL.LoadIdentity();
            GL.Translate(-offset.X, -offset.Y, 0);

            World.Draw();
            if(leftDown)
            {
                RectangleF rect = getRect(pos1, pos2, 32);
                Renderer.FillRect(rect, Color.FromArgb(100, 0, 100, 255));
            }

            control.SwapBuffers();
        }

        public void SetType(Type type)
        {
            this.type = type;
        }

        public void SetWorld(World world)
        {
            this.World = world;
            SetSize();
            control.Invalidate();
        }
    }
}
