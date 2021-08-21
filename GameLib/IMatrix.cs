using System.Numerics;


namespace GameLib
{
    public interface IMatrix
    {
        void SetTranslation(Vector2 translation);
        void SetRotation(float degrees);
        void SetScale(Vector2 scale);
        IMatrix Multiply(IMatrix matrix);
    }
}
