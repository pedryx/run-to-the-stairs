using GameLib;
using GameLib.Systems;

using RunToTheStairs.Systems;
using RunToTheStairs.WPF.Input;
using RunToTheStairs.WPF.Math;

using SkiaSharp;
using SkiaSharp.Views.WPF;

using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Windows.Input;


namespace RunToTheStairs.WPF
{
    class RunToTheStairsWPFGame
    {
        private readonly Dictionary<Key, GameButton> gameButtons_ = new();
        private readonly TextureManager textureManager_ = new();
        private readonly object gameLoopLock_ = new();
        private readonly SKColor clearColor_ = SKColors.Green;
        private readonly SKElement canvas_;

        private RunToTheStairsGame game_;
        private float deltaTime_;
        private bool shouldRender_;
        private DebugGridSystem debugGridSystem_;
        private DebugDiagonalsSystem debugDiagonalsSystem_;
        private Direction playerMoveDirection_;
        private bool playerMove_;

        public RunToTheStairsWPFGame(SKElement canvas)
        {
            canvas_ = canvas;
        }

        public void Initialize()
        {
            Logger.ShowConsole();
            GlobalSettings.Resolution = new Vector2()
            {
                X = (float)canvas_.RenderSize.Width,
                Y = (float)canvas_.RenderSize.Height,
            };

            game_ = new RunToTheStairsGame(
                textureManager_,
                new SkiaMathProvider(),
                new ApperanceProvider()
            );
            textureManager_.LoadAll();
            game_.Initialize();

            debugGridSystem_ = new DebugGridSystem(game_.Camera, game_.Grid);
            debugDiagonalsSystem_ = new DebugDiagonalsSystem(game_.Camera);

            var gameLoopThread = new Thread(GameLoop)
            {
                IsBackground = true,
                Name = "Game Loop Thread",
            };
            gameLoopThread.Start();

            InitializeGameButtons();
        }

        public void Render(SKCanvas canvas)
        {
            if (!shouldRender_)
                return;

            var renderer = new SkiaRenderer(canvas, textureManager_)
            {
                ClearColor = clearColor_,
            };

            game_.Render(deltaTime_, renderer);
            shouldRender_ = false;

            Monitor.Enter(gameLoopLock_);
            Monitor.Pulse(gameLoopLock_);
            Monitor.Exit(gameLoopLock_);
        }

        public void HandleInput(Key key, bool isDown)
        {
            if (gameButtons_.ContainsKey(key))
            {
                gameButtons_[key].Update(isDown);
            }
        }

        private void GameLoop()
        {
            while (true)
            {
                deltaTime_ = game_.CalcDeltaTime();

                if (playerMove_)
                {
                    game_.MovePlayer(playerMoveDirection_);
                    playerMove_ = false;
                }

                game_.Update(deltaTime_);
                shouldRender_ = true;
                canvas_.Dispatcher.Invoke(() => canvas_.InvalidateVisual());

                Monitor.Enter(gameLoopLock_);
                Monitor.Wait(gameLoopLock_);
            }
        }

        private void InitializeGameButtons()
        {
            #region Debug buttons
            CreateDebugSystemButton(Key.F1, debugGridSystem_);
            CreateDebugSystemButton(Key.F2, debugDiagonalsSystem_);
            #endregion
            #region Player control buttons
            CreatePlayerControlButton(Key.NumPad1, Direction.DownLeft);
            CreatePlayerControlButton(Key.NumPad2, Direction.Down);
            CreatePlayerControlButton(Key.NumPad3, Direction.DownRight);
            CreatePlayerControlButton(Key.NumPad4, Direction.Left);
            CreatePlayerControlButton(Key.NumPad6, Direction.Right);
            CreatePlayerControlButton(Key.NumPad7, Direction.UpLeft);
            CreatePlayerControlButton(Key.NumPad8, Direction.Up);
            CreatePlayerControlButton(Key.NumPad9, Direction.UpRight);
            #endregion
        }

        private void CreateDebugSystemButton<TSystem>(Key key, TSystem system)
            where TSystem : RenderSystem
        {
            var button = new GameButton();
            button.OnPress += (sender, e) =>
            {
                if (button.Active)
                    game_.AddRenderSystem(system);
                else
                    game_.RemoveRenderSystem<TSystem>();
            };
            gameButtons_.Add(key, button);
        }

        private void CreatePlayerControlButton(Key key, Direction direction)
        {
            var button = new GameButton();
            button.OnPress += (sender, e) =>
            {
                playerMoveDirection_ = direction;
                playerMove_ = true;
            };
            gameButtons_.Add(key, button);
        }
    }
}
