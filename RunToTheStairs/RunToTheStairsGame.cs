using GameLib;
using GameLib.Systems;

using RunToTheStairs.Systems;

using System.Numerics;
using System.Collections.Generic;


namespace RunToTheStairs
{
    public class RunToTheStairsGame : Game
    {

        protected override void PreInitialize()
        {
            TypeFinder.RegisterAssembly(GetType().Assembly);
        }

        protected override void PostInitialize()
        {

        }

        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {
            return new List<GameSystem>()
            {
                //new GridSystem(5, 5, 80, 80, Vector2.Zero),
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
