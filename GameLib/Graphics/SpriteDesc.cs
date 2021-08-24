namespace GameLib.Graphics
{
    /// <summary>
    /// Contains description of sprite.
    /// </summary>
    public class SpriteDesc
    {
        /// <summary>
        /// Texture name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Texture transformation.
        /// </summary>
        public Transform Transform { get; set; } = new Transform();

        /// <summary>
        /// Source rectangle of texture.
        /// </summary>
        public Rectangle? Clip { get; set; }
    }
}
