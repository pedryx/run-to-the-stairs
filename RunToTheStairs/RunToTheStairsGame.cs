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
        private readonly IApperanceProvider apperanceProvider_;
        private readonly GridPlayerSystem gridPlayerSystem_ = new();

        public Grid Grid { get; private set; }

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
            GlobalSettings.WaitingForInput = true;

            Grid = new Grid(new Vector2(10, 10), new Vector2(64), new Vector2(0));
        }

        protected override void PostInitialize()
        {
            var generator = new GridGenerator(this, Grid, apperanceProvider_);

            var player = generator.SpawnEntities();

            Camera.Follow(player);
        }

        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {

            return new List<GameSystem>()
            {
                new AnimationSystem(),
                new GridSystem(Grid),
                new GridAnimationSystem(),
                new GridAISystem(),
                gridPlayerSystem_,
            };
        }

        protected override IEnumerable<RenderSystem> InitializeRenderSystems()
        {
            return new List<RenderSystem>()
            {
                new SpriteRenderSystem(Camera),
            };
        }

        public void MovePlayer(Direction direction)
            => gridPlayerSystem_.SetMove(direction);
    }
}
