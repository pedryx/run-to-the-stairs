using GameLib;

using SkiaSharp;


namespace RunToTheStairs.WPF
{
    public static class SkiaExtensions
    {

        public static SKRect ToSKRect(this Rectangle rectangle) => new()
        {
            Left = rectangle.X,
            Top = rectangle.Y,
            Size = new SKSize(rectangle.Width, rectangle.Height),
        };
    }
}
