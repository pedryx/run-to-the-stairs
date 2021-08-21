using GameLib;

using SkiaSharp;

using System.Numerics;


namespace RunToTheStairs.WPF
{
    public static class SkiaExtensions
    {
        public static SKMatrix ToSKMatrix(this Matrix4x4 matrix)
            => new SKMatrix()
            {
                ScaleX = matrix.M11,
                SkewX = matrix.M12,
                SkewY = matrix.M21,
                ScaleY = matrix.M22,
                TransX = matrix.M41,
                TransY = matrix.M42,
                Persp2 = 1,
            };

        public static SKRect ToSKRect(this Rectangle rectangle)
            => new SKRect()
            {
                Left = rectangle.X,
                Top = rectangle.Y,
                Size = new SKSize(rectangle.Width, rectangle.Height),
            };
    }
}
