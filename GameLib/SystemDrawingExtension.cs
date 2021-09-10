using System.Drawing;
using System.Numerics;


namespace GameLib
{
    public static class SystemDrawingExtension
    {
        public static Vector2 ToVector(this Point point) => new(point.X, point.Y);

        public static Point ToPoint(this Vector2 vector) => new((int)vector.X, (int)vector.Y);
    }
}
