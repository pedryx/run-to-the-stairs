using GameLib;
using GameLib.Systems;

using RunToTheStairs.Systems;
using RunToTheStairs.WPF.Input;
using RunToTheStairs.WPF.Math;

using SkiaSharp;
using SkiaSharp.Views.WPF;

using System;
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
        private readonly SKColor clearColor_ = SKColors.Black;
        private readonly SKElement canvas_;
        private readonly Random random_ = new();

        private RunToTheStairsGame game_;
        private DebugGridSystem debugGridSystem_;
        private DebugDiagonalsSystem debugDiagonalsSystem_;
        private DebugCameraSystem debugCameraSystem_;

        private bool shouldRender_;
        private Direction playerMoveDirection_;
        private bool playerMove_;
        private Direction? cameraMoveDirection_;
        private Zoom? cameraMoveZoom_;

        public int Seed { get; private set; }

        public float DeltaTime { get; private set; }

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
            Seed = random_.Next();

            game_ = new RunToTheStairsGame(
                textureManager_,
                new SkiaMathProvider(),
                new ApperanceProvider(),
                Seed
            );
            textureManager_.LoadAll();
            game_.Initialize();

            debugGridSystem_ = new DebugGridSystem(game_.Camera, game_.Grid);
            debugDiagonalsSystem_ = new DebugDiagonalsSystem(game_.Camera);
            debugCameraSystem_ = new DebugCameraSystem(game_.Camera);

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

            game_.Render(DeltaTime, renderer);
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
                DeltaTime = game_.CalcDeltaTime(30);

                if (playerMove_)
                {
                    game_.MovePlayer(playerMoveDirection_);
                    playerMove_ = false;
                }

                if (game_.ContainsRenderSystem<DebugCameraSystem>())
                {
                    if (cameraMoveDirection_.HasValue)
                    {
                        debugCameraSystem_.SetMove(cameraMoveDirection_.Value);
                        cameraMoveDirection_ = null;
                    }
                    if (cameraMoveZoom_.HasValue)
                    {
                        debugCameraSystem_.SetZoom(cameraMoveZoom_.Value);
                        cameraMoveZoom_ = null;
                    }
                }

                game_.Update(DeltaTime);
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
            CreateDebugCameraSystemButton(Key.F3);
            #endregion
            #region Debug camera control buttons
            CreateCameraDirectionControlButton(Key.W, Direction.Up);
            CreateCameraDirectionControlButton(Key.A, Direction.Left);
            CreateCameraDirectionControlButton(Key.S, Direction.Down);
            CreateCameraDirectionControlButton(Key.D, Direction.Right);
            CreateCameraZoomControlButton(Key.OemPlus, Zoom.In);
            CreateCameraZoomControlButton(Key.OemMinus, Zoom.Out);
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

        private void CreateDebugCameraSystemButton(Key key)
        {
            var button = new GameButton();
            button.OnPress += (sender, e) =>
            {
                if (button.Active)
                {
                    game_.Camera.Following = false;
                    game_.AddRenderSystem(debugCameraSystem_);
                }
                else
                {
                    game_.Camera.Following = true;
                    game_.RemoveRenderSystem<DebugCameraSystem>();
                }
            };
            gameButtons_.Add(key, button);
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

        private void CreateCameraDirectionControlButton(Key key, Direction direction)
        {
            var button = new GameButton();
            button.OnPress += (sender, e) =>
            {
                cameraMoveDirection_ = direction;
            };
            button.OnRelease += (sender, e) =>
            {
                cameraMoveDirection_ = Direction.None;
            };
            gameButtons_.Add(key, button);
        }

        private void CreateCameraZoomControlButton(Key key, Zoom zoom)
        {
            var button = new GameButton();
            button.OnPress += (sender, e) =>
            {
                cameraMoveZoom_ = zoom;
            };
            button.OnRelease += (sender, e) =>
            {
                cameraMoveZoom_ = Zoom.None;
            };
            gameButtons_.Add(key, button);
        }
    }
}
