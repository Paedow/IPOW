using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace IPOW.Editor
{
    public class Renderer
    {
        public static void Clear(Color color)
        {
            GL.ClearColor(color);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public static void FillRect(RectangleF rect, Color color)
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(rect.Left, rect.Top);
            GL.Vertex2(rect.Right, rect.Top);
            GL.Vertex2(rect.Right, rect.Bottom);
            GL.Vertex2(rect.Left, rect.Bottom);
            GL.End();
        }

        public static void FillRectTexCoord(RectangleF rect, Color color, float tilingX, float tilingY)
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(rect.Left, rect.Top);
            GL.TexCoord2(tilingX, 0); GL.Vertex2(rect.Right, rect.Top);
            GL.TexCoord2(tilingX, tilingY); GL.Vertex2(rect.Right, rect.Bottom);
            GL.TexCoord2(0, tilingY); GL.Vertex2(rect.Left, rect.Bottom);
            GL.End();
        }

        public static void DrawImage(RectangleF rect, Texture texture)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture.GLid);
            FillRectTexCoord(rect, Color.White, 1, 1);
            GL.Disable(EnableCap.Texture2D);
        }

        public static void DrawImage(RectangleF rect, Texture texture, SizeF destSize)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture.GLid);
            FillRectTexCoord(rect, Color.White, rect.Width / destSize.Width, rect.Height / destSize.Height);
            GL.Disable(EnableCap.Texture2D);
        }

        public static void SetBlendFunc()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public static void SetViewport(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
