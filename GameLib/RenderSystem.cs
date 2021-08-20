using System;

namespace GameLib
{
    /// <summary>
    /// Represent a base class for game system which are responsible for rendering.
    /// </summary>
    public abstract class RenderSystem : BaseSystem
    {
        protected RenderSystem(params Type[] supportedTypes) 
            : base(supportedTypes) { }

        /// <summary>
        /// Render part of the game.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        public abstract void Render(float deltaTime, IRenderer renderer);
    }
}
