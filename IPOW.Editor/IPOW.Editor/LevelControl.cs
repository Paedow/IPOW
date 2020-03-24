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

namespace IPOW.Editor
{
    public partial class LevelControl : UserControl
    {
        GLControl control;
        bool devenv;
        int levelW, levelH;
        World world;
        int scrollBarFactor = 5;

        public LevelControl()
        {
            InitializeComponent();

            control = new GLControl();
            control.Dock = DockStyle.Fill;
            container.Controls.Add(control);

            devenv = Process.GetCurrentProcess().ProcessName == "devenv";

            if (!devenv)
                control.Paint += Control_Paint;

            this.Resize += LevelControl_Resize;
            xScroll.Scroll += XScroll_Scroll;
            yScroll.Scroll += YScroll_Scroll;

            world = new World(25, 15);
            SetSize();

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
            this.levelW = world.Width * 32;
            this.levelH = world.Height * 32;

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

            Renderer.Clear(SystemColors.ControlDark);
            Renderer.SetViewport(control.Width, control.Height);

            int offsetX = xScroll.Value * scrollBarFactor - 64;
            int offsetY = yScroll.Value * scrollBarFactor - 64;
            GL.LoadIdentity();
            GL.Translate(-offsetX, -offsetY, 0);

            world.Draw();

            control.SwapBuffers();
        }
    }
}
