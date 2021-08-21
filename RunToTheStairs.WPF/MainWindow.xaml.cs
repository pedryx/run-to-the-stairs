using RunToTheStairs.WPF.Managers;

using SkiaSharp;
using SkiaSharp.Views.Desktop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RunToTheStairs.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RunToTheStairsGame game_ = new();
        private readonly TextureManager textureManager_ = new();
        private readonly object gameLoopLock_ = new();
        private readonly SKColor clearColor_ = SKColors.SkyBlue;

        private float deltaTime_;
        private bool shouldRender_;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game_.Initialize();
            textureManager_.LoadAll();

            var gameLoopThread = new Thread(GameLoop)
            {
                IsBackground = true,
                Name = "Game Loop Thread",
            };
            gameLoopThread.Start();
        }

        private void GameLoop()
        {
            while (true)
            {
                deltaTime_ = game_.CalcDeltaTime();

                game_.Update(deltaTime_);
                shouldRender_ = true;
                gameCanvas.Dispatcher.Invoke(() => gameCanvas.InvalidateVisual());

                Monitor.Enter(gameLoopLock_);
                Monitor.Wait(gameLoopLock_);
            }
        }

        private void GameCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (shouldRender_)
            {
                var renderer = new SkiaRenderer(e.Surface.Canvas, textureManager_)
                {
                    ClearColor = clearColor_,
                };

                game_.Render(deltaTime_, renderer);
                shouldRender_ = false;

                Monitor.Enter(gameLoopLock_);
                Monitor.Pulse(gameLoopLock_);
                Monitor.Exit(gameLoopLock_);
            }
        }

        
    }
}
