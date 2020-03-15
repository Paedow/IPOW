using Godot;
using System;

namespace Pathing
{
    public class Spline
    {
        int length;
        float[] a, b, c, d;

        public Spline(float[] y)
        {
            length = y.Length - 1;
            int n = y.Length - 1;
            float h = 1;

            float[] sigma = new float[n + 1];
            for (int i = 1; i < n; i++)
            {
                float p = (y[i + 1] - 2 * y[i] + y[i - 1]) * 6f / (h * h);
                sigma[i] = (p - sigma[i - 1]) / 4f;
            }

            a = new float[n];
            b = new float[n];
            c = new float[n];
            d = new float[n];

            for (int i = 0; i < n; i++)
            {
                a[i] = (sigma[i + 1] - sigma[i]) / (6f * h);
                b[i] = sigma[i] / 2f;
                c[i] = (y[i + 1] - y[i]) / h - h / 6f * (2f * sigma[i] + sigma[i + 1]);
                d[i] = y[i];
            }
        }

        float pow(float v, int e)
        {
            switch (e)
            {
                case 0: return 1;
                case 1: return v;
                case 2: return v * v;
                case 3: return v * v * v;
                default: return (float)Math.Pow(v, e);
            }
        }

        public float GetVal(float t)
        {
            int i = (int)t;
            if (i < 0) i = 0;
            if (i >= length) i = length - 1;
            float v = a[i] * pow(t - i, 3) + b[i] * pow(t - i, 2) + c[i] * (t - i) + d[i];
            return v;
        }
    }
}