using GameLib;
using GameLib.Graphics;
using GameLib.Systems;

using RunToTheStairs.Systems;

using System.Collections.Generic;
using System.Numerics;


namespace RunToTheStairs
{
    public class RunToTheStairsGame : Game
    {
        public RunToTheStairsGame(ITextureInfoProvider textureInfoProvider) 
            : base(textureInfoProvider) { }

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
                new GridSystem(5, 5, 80, 80, Vector2.Zero),
                new AnimationSystem(),
                new GridAnimationSystem(),
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
