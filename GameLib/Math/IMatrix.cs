using System.Numerics;

namespace GameLib.Math
{
    public interface IMatrix 
    {
        Vector2 MapPoint(Vector2 point);
    }

    public static class MatrixExtension
    {
        public static IMatrix Concat(this IMatrix matrix1, IMatrix matrix2)
            => GlobalSettings.MathProvider.Concat(matrix1, matrix2);
    }

    public static class Matrix
    {
        public static IMatrix GetIdentity()
            => GlobalSettings.MathProvider.GetIdentityMatrix();
    }
}
