using GameLib.Math;

using SkiaSharp;

using System.Numerics;


namespace RunToTheStairs.WPF.Math
{
    /// <summary>
    /// Represent a wrapper around <see cref="SKMatrix"/>.
    /// </summary>
    class SKMatrixWrapper : IMatrix
    {
        public SKMatrixWrapper()
            : this(SKMatrix.Identity) { }

        public SKMatrixWrapper(SKMatrix matrix)
        {
            Matrix = matrix;
        }

        public Vector2 MapPoint(Vector2 point)
            => Matrix.MapPoint(point.ToSKPoint()).ToVector2();

        public SKMatrix Matrix { get; private set; }

        public static implicit operator SKMatrix(SKMatrixWrapper wrapper)
            => wrapper.Matrix;

        public static implicit operator SKMatrixWrapper(SKMatrix matrix)
            => new(matrix);
    }
}
