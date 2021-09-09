using GameLib;
using GameLib.Graphics;
using GameLib.Math;

using RunToTheStairs.WPF.Math;

using SkiaSharp;

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

        public void Render(string textureName, IMatrix transform, System.Drawing.Color color, Rectangle? clip = null)
        {
            SKBitmap bitmap = textureManager_[textureName];
            var paint = new SKPaint()
            {
                IsAntialias = true,
                ColorFilter = SKColorFilter.CreateBlendMode
                (
                    color.ToSKColor(),
                    SKBlendMode.Modulate
                ),
            };

            canvas_.SetMatrix((SKMatrixWrapper)transform);
            if (clip == null)
            {
                canvas_.DrawBitmap(bitmap, new SKPoint(0, 0), paint);
            }
            else
            {
                SKRect source = clip.Value.ToSKRect();
                SKRect destination = new(0, 0, clip.Value.Width, clip.Value.Height);

                canvas_.DrawBitmap(bitmap, source, destination, paint);
            }
        }

        public IPrimitivesRenderer GetPrimitivesRenderer() => this;

        public void SetColor(System.Drawing.Color color)
            => DrawColor = color.ToSKColor();

        public void DrawLine(Vector2 start, Vector2 end, float stroke, IMatrix transform)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.SetMatrix((SKMatrixWrapper)transform);
            canvas_.DrawLine(start.X, start.Y, end.X, end.Y, brush_);
        }

        public void DrawRectangle(Rectangle rectangle, float stroke, IMatrix transform)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.SetMatrix((SKMatrixWrapper)transform);
            canvas_.DrawRect(rectangle.ToSKRect(), brush_);
        }

        public void DrawEllipse(Rectangle rectangle, float stroke, IMatrix transform)
        {
            brush_.StrokeWidth = stroke;
            brush_.Style = SKPaintStyle.Stroke;
            canvas_.SetMatrix((SKMatrixWrapper)transform);
            canvas_.DrawOval(rectangle.ToSKRect(), brush_);
        }

        public void FillRectangle(Rectangle rectangle, IMatrix transform)
        {
            brush_.Style = SKPaintStyle.Fill;
            canvas_.SetMatrix((SKMatrixWrapper)transform);
            canvas_.DrawRect(rectangle.ToSKRect(), brush_);
        }

        public void FillEllipse(Rectangle rectangle, IMatrix transform)
        {
            brush_.Style = SKPaintStyle.Fill;
            canvas_.SetMatrix((SKMatrixWrapper)transform);
            canvas_.DrawOval(rectangle.ToSKRect(), brush_);
        }
    }
}
