using GameLib;
using GameLib.Systems;

using System.Collections.Generic;


namespace RunToTheStairs
{
    public class RunToTheStairsGame : Game
    {
        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {
            return new List<GameSystem>()
            {

            };
        }

        protected override IEnumerable<RenderSystem> InitializeRenderSystems()
        {
            return new List<RenderSystem>()
            {
                new SpriteRenderSystem(),
            };
        }
    }
}
