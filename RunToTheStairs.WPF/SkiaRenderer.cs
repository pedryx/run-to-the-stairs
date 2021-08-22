using GameLib;

using RunToTheStairs.WPF.Managers;

using SkiaSharp;

using System.Drawing;
using System.Numerics;


namespace RunToTheStairs.WPF
{
    public class SkiaRenderer : IRenderer, IPrimitivesRenderer
    {
        private readonly SKCanvas canvas_;
        private readonly TextureManager textureManager_;

        private readonly SKPaint brush_ = new();

        public SKColor ClearColor { get; set; }

        public SKColor DrawColor
        {
            get => brush_.Color;
            set => brush_.Color = value;
        }

        public SkiaRenderer(SKCanvas canvas, TextureManager textureManager)
        {
            canvas_ = canvas;
            textureManager_ = textureManager;
        }

        public void Clear()
        {
            canvas_.Clear(ClearColor);
        }

        public void StartRender() { }

        public void EndRender() { }

        public void Render(Sprite sprite, Transform transform, GameLib.Rectangle? clip = null)
        {
            SKBitmap bitmap = textureManager_[sprite.Name];

            canvas_.SetMatrix(CalcTransform(transform, sprite.Transform));
            if (clip == null)
            {
                canvas_.DrawBitmap(bitmap, new SKPoint(0, 0));
            }
            else
            {
                SKRect source = clip.Value.ToSKRect();
                SKRect destination = new(0, 0, clip.Value.Width, clip.Value.Height);

                canvas_.DrawBitmap(bitmap, source, destination);
            }
        }

        public IPrimitivesRenderer GetPrimitivesRenderer() => this;

        private static SKMatrix CalcTransform(Transform entitiyTransform, Transform spriteTransform)
        {
            SKMatrixWrapper entityMatrix 
                = (SKMatrixWrapper)entitiyTransform.GetMatrix<SKMatrixWrapper>();
            SKMatrixWrapper spriteMatrix
                = (SKMatrixWrapper)spriteTransform.GetMatrix<SKMatrixWrapper>();

            SKMatrixWrapper result = (SKMatrixWrapper)entityMatrix.Multiply(spriteMatrix);
            return SKMatrix.Concat(result.Matrix, SKMatrix.CreateScale(GlobalSettings.Scale, GlobalSettings.Scale));
        }

        public void SetColor(Color color)
        {
            DrawColor = new SKColor(color.R, color.G, color.B, color.A);
        }

        public void DrawLine(Vector2 start, Vector2 end, float stroke)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.DrawLine(start.X, start.Y, end.X, end.Y, brush_);
        }

        public void DrawRectangle(GameLib.Rectangle rectangle, float stroke)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.DrawRect(rectangle.ToSKRect(), brush_);
        }

        public void DrawEllipse(GameLib.Rectangle rectangle, float stroke)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.DrawOval(rectangle.ToSKRect(), brush_);
        }

        public void FillRectangle(GameLib.Rectangle rectangle)
        {
            brush_.Style = SKPaintStyle.Fill;
            canvas_.DrawRect(rectangle.ToSKRect(), brush_);
        }

        public void FillEllipse(GameLib.Rectangle rectangle)
        {
            brush_.Style = SKPaintStyle.Fill;
            canvas_.DrawOval(rectangle.ToSKRect(), brush_);
        }
    }
}
