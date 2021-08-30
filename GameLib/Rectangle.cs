using System.Numerics;

namespace GameLib
{
    /// <summary>
    /// Represent an rectangle.
    /// </summary>
    public struct Rectangle
    {
        /// <summary>
        /// X coor of top-left corner.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y coor of top-left corner.
        /// </summary>
        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public Vector2 Position => new(X, Y);

        public Vector2 Size => new(Width, Height);

    }
}
