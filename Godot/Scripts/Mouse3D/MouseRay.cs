using Godot;
using System;
using IPOW.Tiles;

namespace Mouse3D
{
    public class MouseRay
    {
        Vector3 origin, direction;
        Camera camera;

        public MouseRay(Camera camera, Vector2 mouseOnScreen)
        {
            this.camera = camera;
            this.origin = camera.ProjectRayOrigin(mouseOnScreen);
            this.direction = camera.ProjectRayNormal(mouseOnScreen);
        }

        public Vector3? PositionOnPlane(Plane plane)
        {
            return plane.IntersectRay(this.origin, this.direction);
        }

        public StaticBody SendRay()
        {
            var spaceState = camera.GetWorld().DirectSpaceState;
            var result = spaceState.IntersectRay(this.origin, this.origin + this.direction * 100);
            if(!result.Contains("collider"))
                return null;
            var collider = (StaticBody)result["collider"];
            return collider;
        }

        public Tile SendRayTile()
        {
            Node node = SendRay();
            if(node == null) return null;
            while(node.GetParent() != null)
            {
                if(node is Tile)
                    return (Tile)node;
                node = node.GetParent();
            }
            return null;
        }
    }
}