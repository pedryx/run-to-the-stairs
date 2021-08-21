using System.Numerics;

namespace GameLib
{
    /// <summary>
    /// Represent an interface for renderers.
    /// </summary>
    public interface IRenderer
    {
        void StartRender();
        void EndRender();

        /// <summary>
        /// Render sprite. Sprite is clipped then transformed then rendered.
        /// </summary>
        /// <param name="clip">Clip of the sprite.</param>
        void Render(string textureName, Matrix4x4 transform, Rectangle? clip = null);
    }
}
