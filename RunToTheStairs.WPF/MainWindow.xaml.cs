using SkiaSharp.Views.Desktop;

using System.Windows;
using System.Windows.Input;


namespace RunToTheStairs.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RunToTheStairsWPFGame game_;

        public MainWindow()
        {
            InitializeComponent();

            game_ = new RunToTheStairsWPFGame(gameCanvas);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game_.Initialize();
        }

        private void GameCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            game_.Render(e.Surface.Canvas);

            Title = $"Run To The Stairs (Seed = {game_.Seed}, FPS = {1 / game_.DeltaTime : 0})";
        }

        private void Window_KeyHandle(object sender, KeyEventArgs e)
        {
            game_.HandleInput(e.Key, e.IsDown);
        }
    }
}
