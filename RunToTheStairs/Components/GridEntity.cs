using GameLib;

using System.Numerics;


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
        public Direction Movement { get; set; }
    }
}
