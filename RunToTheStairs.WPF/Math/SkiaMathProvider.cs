using GameLib;
using GameLib.Math;

using SkiaSharp;


namespace RunToTheStairs.WPF.Math
{
    /// <summary>
    /// Represent a math provider of Skia math.
    /// </summary>
    class SkiaMathProvider : IMathProvider
    {
        public IMatrix Concat(IMatrix matrix1, IMatrix matrix2)
        {
            SKMatrixWrapper skMatrix1 = (SKMatrixWrapper)matrix1;
            SKMatrixWrapper skMatrix2 = (SKMatrixWrapper)matrix2;

            return (SKMatrixWrapper)SKMatrix.Concat(skMatrix1, skMatrix2);
        }

        public IMatrix MatrixFromTransform(Transform transform)
        {
            SKMatrix translation = SKMatrix.CreateTranslation(
                transform.Position.X,
                transform.Position.Y
            );
            SKMatrix scale = SKMatrix.CreateScale(transform.Scale.X, transform.Scale.Y);
            SKMatrix rotation = SKMatrix.CreateRotationDegrees(transform.Rotation);

            SKMatrix result = SKMatrix.Concat(translation, scale);
            result = SKMatrix.Concat(result, rotation);

            return (SKMatrixWrapper)result;
        }

        public IMatrix GetIdentityMatrix()
            => (SKMatrixWrapper)SKMatrix.Identity;
    }
}
