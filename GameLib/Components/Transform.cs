using GameLib.Math;

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

        public Vector2 Scale { get; set; } = Vector2.One;

        public IMatrix GetMatrix(IMathProvider provider)
            => provider.MatrixFromTransform(this);

        public IMatrix GetMatrix()
            => GetMatrix(GlobalSettings.MathProvider);
    }
}
