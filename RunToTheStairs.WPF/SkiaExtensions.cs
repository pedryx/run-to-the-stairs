using GameLib;

using SkiaSharp;


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
        /// Convert <see cref="System.Drawing.Color"/> to <see cref="SKColor"/>.
        /// </summary>
        public static SKColor ToSKColor(this System.Drawing.Color color) 
            => new(color.R, color.G, color.B, color.A);
    }
}
