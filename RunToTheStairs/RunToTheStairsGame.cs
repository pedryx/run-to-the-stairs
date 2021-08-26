using GameLib;
using GameLib.Graphics;
using GameLib.Math;
using GameLib.Systems;

using RunToTheStairs.Systems;

using System.Collections.Generic;
using System.Numerics;


namespace RunToTheStairs
{
    public class RunToTheStairsGame : Game
    {
        private Grid grid_;

        public RunToTheStairsGame(
            ITextureInfoProvider textureInfoProvider,
            IMathProvider mathProvider
         ) 
            : base(textureInfoProvider, mathProvider) { }

        protected override void PreInitialize()
        {
            TypeFinder.RegisterAssembly(GetType().Assembly);

            grid_ = new Grid(new Vector2(10, 10), new Vector2(64), new Vector2(0));
        }

        protected override void PostInitialize()
        {
            var factory = new EntityFactory(this, grid_);

            var skeleton1 = factory.CreateSkeleton("skeleton1", new Vector2(0, 0));
            var skeleton2 = factory.CreateSkeleton("skeleton2", new Vector2(2, 2));
            var skeleton3 = factory.CreateSkeleton("skeleton3", new Vector2(4, 4));

            Camera.Follow(skeleton2);
        }

        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {
            return new List<GameSystem>()
            {
                new AnimationSystem(),
                new GridSystem(grid_),
                new GridAnimationSystem(),
                new GridAISystem(),
            };
        }

        protected override IEnumerable<RenderSystem> InitializeRenderSystems()
        {
            return new List<RenderSystem>()
            {
                new SpriteRenderSystem(Camera),
                new DebugGridSystem(Camera, grid_),
            };
        }
    }
}
