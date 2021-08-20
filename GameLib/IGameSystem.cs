namespace GameLib
{
    /// <summary>
    /// Represent an interface for a game system for updateting game logic.
    /// </summary>
    public interface IGameSystem
    {
        /// <summary>
        /// Update part of the game logic.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        void Update(float deltaTime);
    }
}
