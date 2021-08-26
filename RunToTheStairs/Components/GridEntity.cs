using GameLib;

using System.Numerics;
using System.Xml.Serialization;


namespace RunToTheStairs.Components
{
    /// <summary>
    /// Represent an entity on the grid.
    /// </summary>
    public class GridEntity : IComponent
    {
        /// <summary>
        /// Position of tile on which is entity standing.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Represent a direction of movement.
        /// </summary>
        [XmlIgnore]
        public Direction Movement { get; set; } = Direction.None;

        /// <summary>
        /// Number of tiles per second.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Number of seconds ellapsed from last movement.
        /// </summary>
        [XmlIgnore]
        public float Ellapsed { get; set; }
    }
}
