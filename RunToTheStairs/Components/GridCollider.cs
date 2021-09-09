using GameLib;

using System.Xml.Serialization;


namespace RunToTheStairs.Components
{
    public class GridCollider : IComponent
    {
        /// <summary>
        /// Determine if collider is dinamic or static.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Name of the scirpt which will be invoked when collision occur.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// If both colliders have this property set to true then movement will not occur.
        /// </summary>
        public bool PreventMovement { get; set; } = true;

        /// <summary>
        /// Determine if collision occured during last iteration.
        /// </summary>
        [XmlIgnore]
        public bool CollisionOccured { get; set; }
    }
}
