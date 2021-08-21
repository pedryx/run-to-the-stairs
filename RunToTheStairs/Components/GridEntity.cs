using GameLib;


namespace RunToTheStairs.Components
{
    /// <summary>
    /// Represent an entity on the grid.
    /// </summary>
    class GridEntity : IComponent
    {
        /// <summary>
        /// Represent a X coor position of entity on the grid.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Represent a Y coor position of entity on the grid.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Represent a direction of movement.
        /// </summary>
        public Direction Movement { get; set; }
    }
}
