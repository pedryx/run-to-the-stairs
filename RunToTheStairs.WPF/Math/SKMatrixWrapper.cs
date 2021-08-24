using GameLib.Math;

using SkiaSharp;


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

        public SKMatrix Matrix { get; private set; }

        public static implicit operator SKMatrix(SKMatrixWrapper wrapper)
            => wrapper.Matrix;

        public static implicit operator SKMatrixWrapper(SKMatrix matrix)
            => new(matrix);
    }
}
