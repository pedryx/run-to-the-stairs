using System.Collections.Generic;


namespace GameLib.UnitTests
{
    /// <summary>
    /// Represent a fake empty game with no systems and no entities.
    /// </summary>
    class FakeGame : Game
    {
        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {
            return new List<GameSystem>();
        }

        protected override IEnumerable<RenderSystem> InitializeRenderSystems()
        {
            return new List<RenderSystem>();
        }
    }
}
