using Godot;

namespace IPOW.Tiles
{
    public class MinimapColors
    {
        public static Color TILE { get; private set; } = Color.Color8(0x26,0x7F,0x00);
        public static Color TILE2 { get; private set; } = Color.Color8(0x00,0x7F,0x46);
        public static Color TOWER { get; private set; } = Color.Color8(150, 150, 150);
        public static Color COBBLE { get; private set; } = Color.Color8(200, 200, 200);
        public static Color NONE { get; private set; } = Color.Color8(0, 0, 0);
        public static Color CREEP{get;private set;} = Color.Color8(255,0,0);
    }
}