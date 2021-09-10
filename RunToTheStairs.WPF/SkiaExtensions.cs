using SkiaSharp;

using System.Drawing;
using System.Numerics;


namespace RunToTheStairs.WPF
{
    public static class SkiaExtensions
    {
        /// <summary>
        /// Convert <see cref="Rectangle"/> to <see cref="SKRect"/>.
        /// </summary>
        public static SKRect ToSKRect(this Rectangle rectangle) => new()
        {
            Left = rectangle.X,
            Top = rectangle.Y,
            Size = new SKSize(rectangle.Width, rectangle.Height),
        };

        /// <summary>
        /// Convert <see cref="Color"/> to <see cref="SKColor"/>.
        /// </summary>
        public static SKColor ToSKColor(this Color color) 
            => new(color.R, color.G, color.B, color.A);

        /// <summary>
        /// Convert Vector2 to SKPoin.
        /// </summary>
        public static SKPoint ToSKPoint(this Vector2 vector)
            => new(vector.X, vector.Y);

        /// <summary>
        /// Convert SKPoint to Vector2.
        /// </summary>
        public static Vector2 ToVector2(this SKPoint point)
            => new(point.X, point.Y);
    }
}
