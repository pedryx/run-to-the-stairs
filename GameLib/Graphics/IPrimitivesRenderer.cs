using GameLib.Math;

using System.Drawing;
using System.Numerics;


namespace GameLib
{
    /// <summary>
    /// Represent an interface for renderer of primitive shapes.
    /// </summary>
    public interface IPrimitivesRenderer
    {
        void SetColor(Color color);
        void DrawLine(Vector2 start, Vector2 end, float stroke, IMatrix transform);
        void DrawRectangle(Rectangle rectangle, float stroke, IMatrix transform);
        void DrawEllipse(Rectangle rectangle, float stroke, IMatrix transform);
        void FillRectangle(Rectangle rectangle, IMatrix transform);
        void FillEllipse(Rectangle rectangle, IMatrix transform);
    }
}
