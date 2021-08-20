namespace GameLib
{
    /// <summary>
    /// Represent an interface for game system for rendering the game.
    /// </summary>
    public interface IRenderSystem
    {
        /// <summary>
        /// Render part of the game.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        void Render(float deltaTime, IRenderer renderer);
    }
}
