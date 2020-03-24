using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace IPOW.Editor
{
    public class Texture
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int GLid { get; private set; }

        public Texture(int width, int height)
            : this(width, height, IntPtr.Zero)
        {

        }

        public Texture(int width, int height, IntPtr pixels)
        {
            this.Width = width;
            this.Height = height;
            generate(pixels);
        }

        public Texture(Bitmap bmp)
        {
            this.Width = bmp.Width;
            this.Height = bmp.Height;
            System.Drawing.Imaging.BitmapData bdat = bmp.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            generate(bdat.Scan0);
            bmp.UnlockBits(bdat);
        }

        void generate(IntPtr pixels)
        {
            GLid = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, GLid);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba32ui, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedInt, pixels);
        }
    }
}
