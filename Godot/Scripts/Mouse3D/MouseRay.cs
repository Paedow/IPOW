using Godot;
using System;

namespace Mouse3D
{
    public class MouseRay
    {
        Vector3 origin, direction;

        public MouseRay(Camera camera, Vector2 mouseOnScreen)
        {
            this.origin = camera.ProjectRayOrigin(mouseOnScreen);
            this.direction = camera.ProjectRayNormal(mouseOnScreen);
        }

        public Vector3? PositionOnPlane(Plane plane)
        {
            return plane.IntersectRay(this.origin, this.direction);
        }
    }
}