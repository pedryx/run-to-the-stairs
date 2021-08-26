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
        private IApperanceProvider apperanceProvider_;

        public RunToTheStairsGame
        (
            ITextureInfoProvider textureInfoProvider,
            IMathProvider mathProvider,
            IApperanceProvider apperanceProvider
        ) 
            : base(textureInfoProvider, mathProvider) 
        {
            apperanceProvider_ = apperanceProvider;
        }

        protected override void PreInitialize()
        {
            TypeFinder.RegisterAssembly(GetType().Assembly);

            grid_ = new Grid(new Vector2(10, 10), new Vector2(64), new Vector2(0));
        }

        protected override void PostInitialize()
        {
            GridGenerator generator = new GridGenerator(this, grid_, apperanceProvider_);

            var player = generator.SpawnEntities();

            Camera.Follow(player);
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
