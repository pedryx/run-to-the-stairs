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
        private readonly GridCollisionsSystem gridCollisionsSystem_;
        private readonly int? seed_;

        private Scene scene_;

        public Grid Grid { get; private set; }

        public RunToTheStairsGame
        (
            ITextureInfoProvider textureInfoProvider,
            IMathProvider mathProvider,
            IApperanceProvider apperanceProvider,
            int? seed = null
        )
            : base(textureInfoProvider, mathProvider)
        {
            apperanceProvider_ = apperanceProvider;
            seed_ = seed;

            gridCollisionsSystem_ = new GridCollisionsSystem(this);
        }

        protected override void PreInitialize()
        {
            TypeFinder.RegisterAssembly(GetType().Assembly);
            Grid = new Grid(new Vector2(90), new Vector2(64), new Vector2(0));
            scene_ = new Scene(this, Grid, apperanceProvider_);
            //Camera.GameTransform.Scale = new Vector2(.5f); // todo: fix zoom out and came follow
        }

        protected override void PostInitialize()
        {
            Entity player = scene_.Create(seed_);

            gridCollisionsSystem_.UpdatePhysics();
            Camera.Follow(player);
        }

        protected override IEnumerable<GameSystem> InitializeGameSystems()
        {
            return new List<GameSystem>()
            {
                gridPlayerSystem_,
                new GridAISystem(scene_),
                gridCollisionsSystem_,
                new GridAnimationSystem(),
                new AnimationSystem(),
                new GridSystem(Grid),
                new PathHighlightSystem(scene_),
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
