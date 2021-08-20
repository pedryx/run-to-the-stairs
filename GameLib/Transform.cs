using System.Numerics;


namespace GameLib
{
    /// <summary>
    /// Represent a trasformation information.
    /// </summary>
    public class Transform : IComponent
    {

        public Vector2 Position { get; set; }

        public Vector2 Rotation { get; set; }

        public Vector2 Scale { get; set; }

    }
}
