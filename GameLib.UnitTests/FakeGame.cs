using System.Collections.Generic;


namespace GameLib.UnitTests
{
    /// <summary>
    /// Represent a fake empty game with no systems and no entities.
    /// </summary>
    class FakeGame : Game
    {
        protected override IEnumerable<IGameSystem> InitializeGameSystems()
        {
            return new List<IGameSystem>();
        }

        protected override IEnumerable<IRenderSystem> InitializeRenderSystems()
        {
            return new List<IRenderSystem>();
        }
    }
}
