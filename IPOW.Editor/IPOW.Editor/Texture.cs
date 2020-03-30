using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Runtime.InteropServices;

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
            System.Drawing.Imaging.BitmapData bdat = bmp.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int[] buffer = new int[bmp.Width * bmp.Height];
            Marshal.Copy(bdat.Scan0, buffer, 0, buffer.Length);
            for(int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (int)(((uint)buffer[i] & 0xff00ff00) | (((uint)buffer[i] & 0xff) << 16) | (((uint)buffer[i] & 0xff0000) >> 16));
            }
            IntPtr ptr = Marshal.AllocHGlobal(buffer.Length * 4);
            Marshal.Copy(buffer, 0, ptr, buffer.Length);
            generate(ptr);
            bmp.UnlockBits(bdat);
            Marshal.FreeHGlobal(ptr);
        }

        void generate(IntPtr pixels)
        {
            GLid = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, GLid);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }
    }
}
