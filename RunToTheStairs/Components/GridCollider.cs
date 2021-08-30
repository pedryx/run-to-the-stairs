using GameLib;


namespace RunToTheStairs.Components
{
    public class GridCollider : IComponent
    {
        /// <summary>
        /// Determine if collider is dinamic or static.
        /// </summary>
        public bool IsStatic { get; set; }
    }
}
