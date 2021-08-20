namespace GameLib
{
    /// <summary>
    /// Represent an interface for renderers.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Render sprite. Sprite is clipped then transformed then rendered.
        /// </summary>
        /// <param name="spriteName">Name of the sprite.</param>
        /// <param name="transform">Transformation of the sprite.</param>
        /// <param name="clip">Clip of the sprite.</param>
        void Render(string spriteName, Transform transform, Rectangle? clip = null);
    }
}
