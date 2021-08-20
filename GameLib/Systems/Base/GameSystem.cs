using System;

namespace GameLib
{
    /// <summary>
    /// Represent a base class for a game systems which are responsible for updating game
    /// logic.
    /// </summary>
    public abstract class GameSystem : BaseSystem
    {
        protected GameSystem(params Type[] supportedTypes) 
            : base(supportedTypes) { }

        /// <summary>
        /// Update part of the game logic.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        public abstract void Update(float deltaTime);
    }
}
