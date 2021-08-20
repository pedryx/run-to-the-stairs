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
        /// <param name="sprite">Sprite to render.</param>
        /// <param name="clip">Clip of the sprite.</param>
        void Render(Sprite sprite, Rectangle? clip = null);
    }
}
