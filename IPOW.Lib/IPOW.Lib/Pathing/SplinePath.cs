using Godot;
using System;

namespace IPOWLib.Pathing
{
    public class SplinePath
    {
        InterpolationType interpolationType;
        Vector2[] path;
        Spline splineX, splineY;

        public int Length
        {
            get
            {
                return path.Length;
            }
        }

        public SplinePath(Vector2[] path, InterpolationType interpolationType)
        {
            this.path = path;
            this.interpolationType = interpolationType;
            prepareSpline();
        }

        public SplinePath(PointI[] path, float scale, Vector2 offset, InterpolationType interpolationType)
        {
            this.path = new Vector2[path.Length];
            for (int i = 0; i < path.Length; i++)
            {
                this.path[i] = (Vector2)path[i] * scale + offset;
            }
            this.interpolationType = interpolationType;
            prepareSpline();
        }

        public SplinePath(PointI[] path, float scale, InterpolationType interpolationType)
            : this(path, scale, new Vector2(scale / 2f, scale / 2f), interpolationType) { }

        void prepareSpline()
        {
            if (path.Length == 0)
                this.interpolationType = InterpolationType.Null;
            if (path.Length <= 4 && this.interpolationType == InterpolationType.Qubic)
                this.interpolationType = InterpolationType.Linear;

            if (interpolationType == InterpolationType.Qubic)
            {
                float[] x = new float[path.Length];
                float[] y = new float[path.Length];
                for (int i = 0; i < path.Length; i++)
                {
                    x[i] = path[i].x;
                    y[i] = path[i].y;
                }
                splineX = new Spline(x);
                splineY = new Spline(y);
            }
        }

        public Vector2 GetPoint(float t)
        {
            switch (this.interpolationType)
            {
                case InterpolationType.Linear:
                    {
                        int tInt = (int)t;
                        if (tInt < 0) tInt = 0;
                        if (tInt >= path.Length - 1) tInt = path.Length - 2;
                        if (tInt == t)
                            return path[tInt];
                        Vector2 p1 = path[tInt];
                        Vector2 p2 = path[tInt + 1];
                        Vector2 d = (p2 - p1);
                        return p1 + d * (t - tInt);
                    }
                case InterpolationType.Qubic:
                    {
                        float x = splineX.GetVal(t);
                        float y = splineY.GetVal(t);
                        return new Vector2(x, y);
                    }
                default:
                    return new Vector2(0, 0);
            }
        }
    }

    public enum InterpolationType
    {
        Linear = 1,
        Qubic = 2,
        Null = 3
    }
}