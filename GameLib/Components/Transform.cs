using System;
using System.Numerics;


namespace GameLib
{

    /// <summary>
    /// Represent a trasformation information.
    /// </summary>
    public class Transform : IComponent
    {

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }

        public Matrix4x4 GetMatrix()
            => new Matrix4x4()
            {
                M11 = MathF.Cos(Rotation) * Scale.X,
                M12 = -MathF.Sin(Rotation) * Scale.Y,
                M21 = MathF.Sin(Rotation) * Scale.X,
                M22 = MathF.Cos(Rotation) * Scale.Y,
                M14 = Position.X,
                M24 = Position.Y,
                M33 = 1,
                M44 = 1,
            };

    }
}
