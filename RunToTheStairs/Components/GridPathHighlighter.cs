using GameLib;

using System.Collections.Generic;
using System.Numerics;
using System.Xml.Serialization;


namespace RunToTheStairs.Components
{
    /// <summary>
    /// Represent a highlighter of a path from current entity to the goal in grid.
    /// </summary>
    public class GridPathHighlighter : IComponent
    {
        /// <summary>
        /// Position in grid of current entity during last iteration.
        /// </summary>
        public Vector2 LastPosition { get; set; }

        /// <summary>
        /// Contains all entities which are part of currently highlighted path.
        /// </summary>
        [XmlIgnore]
        public List<Entity> CurrentHighlight { get; set; } = new();
    }
}
