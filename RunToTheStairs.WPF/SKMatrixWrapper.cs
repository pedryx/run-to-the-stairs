using GameLib;

using SkiaSharp;

using System.Numerics;


namespace RunToTheStairs.WPF
{
    public class SKMatrixWrapper : IMatrix
    {
        private SKMatrix matrix_;

        public SKMatrix Matrix
        {
            get => matrix_;
            set { matrix_ = value; }
        }

        public SKMatrixWrapper()
            : this (SKMatrix.Identity) { }

        public SKMatrixWrapper(SKMatrix matrix)
        {
            Matrix = matrix;
        }

        public IMatrix Multiply(IMatrix wrapper)
        {
            SKMatrix result = SKMatrix.Concat(Matrix, (wrapper as SKMatrixWrapper).Matrix);
            return new SKMatrixWrapper(result);
        }

        public void SetRotation(float degrees)
        {
            Matrix = SKMatrix.CreateRotation(degrees);
        }

        public void SetScale(Vector2 scale)
        {
            matrix_.ScaleX = scale.X;
            matrix_.ScaleY = scale.Y;
        }

        public void SetTranslation(Vector2 translation)
        {
            matrix_.TransX = translation.X;
            matrix_.TransY = translation.Y;
        }
    }
}
