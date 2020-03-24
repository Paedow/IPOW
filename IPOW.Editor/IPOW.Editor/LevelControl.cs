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
            SetSize(2000, 2000);
            control.Invalidate();
        }

        public void SetSize(int width, int height)
        {
            this.levelW = width;
            this.levelH = height;

            int scrollMaxY = levelH - control.ClientSize.Height + 128;
            int scrollMaxX = levelW - control.ClientSize.Width + 128;

            if (scrollMaxX > 0)
                xScroll.Maximum = scrollMaxX;
            xScroll.Enabled = scrollMaxX > 0;
            if (scrollMaxY > 0)
                yScroll.Maximum = scrollMaxY;
            yScroll.Enabled = scrollMaxY > 0;
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            if (devenv) return;
            control.MakeCurrent();

            Renderer.Clear(SystemColors.ControlDark);
            Renderer.SetViewport(control.Width, control.Height);

            int offsetX = xScroll.Value - 64;
            int offsetY = yScroll.Value - 64;
            GL.LoadIdentity();
            GL.Translate(-offsetX, -offsetY, 0);

            Renderer.FillRect(new RectangleF(0, 0, levelW, levelH), Color.White);
            Renderer.FillRect(new RectangleF(0, 0, 32, 32), Color.Yellow);
            Renderer.FillRect(new RectangleF(levelW-32, levelH-32, 32, 32), Color.Yellow);

            control.SwapBuffers();
        }
    }
}
