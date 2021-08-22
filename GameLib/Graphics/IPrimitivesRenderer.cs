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
        void DrawLine(Vector2 start, Vector2 end, float stroke);
        void DrawRectangle(Rectangle rectangle, float stroke);
        void DrawEllipse(Rectangle rectangle, float stroke);
        void FillRectangle(Rectangle rectangle);
        void FillEllipse(Rectangle rectangle);
    }
}
