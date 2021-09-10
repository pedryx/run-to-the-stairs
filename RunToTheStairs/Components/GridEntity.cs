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
        public Direction Movement { get; set; }

        /// <summary>
        /// Determine if entity movement is valid.
        /// </summary>
        [XmlIgnore]
        public bool CanMove { get; set; }

        [XmlIgnore]
        public bool Moving { get; set; }

        /// <summary>
        /// Number of tiles per second.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Time in seconds which remeains until next move.
        /// </summary>
        [XmlIgnore]
        public float ReamingTime { get; set; }

        /// <summary>
        /// Number of seconds ellapsed from the start of movement.
        /// </summary>
        [XmlIgnore]
        public float Ellapsed { get; set; }

        public Vector2 GetNextPosition()
            => Position + Movement.ToVector();
    }
}
