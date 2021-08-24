using GameLib.Math;


namespace GameLib.Graphics
{
    /// <summary>
    /// Represent an interface for renderers.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Occur before rendering.
        /// </summary>
        void StartRender();

        /// <summary>
        /// Occur after rendering.
        /// </summary>
        void EndRender();

        /// <summary>
        /// Clear the screen.
        /// </summary>
        void Clear();

        /// <summary>
        /// Render texture.
        /// </summary>
        /// <param name="name">Name of the texture to render.</param>
        /// <param name="matrix">Transformation matrix for texture.</param>
        /// <param name="clip">Source rectangle of texture.</param>
        void Render(string name, IMatrix matrix, Rectangle? clip = null);

        IPrimitivesRenderer GetPrimitivesRenderer();
    }
}
