using GameLib;

using RunToTheStairs.WPF.Managers;

using SkiaSharp;

using System.Numerics;


namespace RunToTheStairs.WPF
{
    public class SkiaRenderer : IRenderer
    {
        private readonly SKCanvas canvas_;
        private readonly TextureManager textureManager_;

        public SKColor ClearColor { get; set; }

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

        public void Render(Sprite sprite, Transform transform, Rectangle? clip = null)
        {
            SKBitmap bitmap = textureManager_[sprite.Name];
            SKRect destination = new(0, 0, bitmap.Width, bitmap.Height);

            canvas_.SetMatrix(CalcTransform(transform, sprite.Transform));
            if (clip == null)
            {
                canvas_.DrawBitmap(bitmap, destination);
            }
            else
            {
                SKRect source = clip.Value.ToSKRect();
                canvas_.DrawBitmap(bitmap, source, destination);
            }
        }

        private static SKMatrix CalcTransform(Transform entitiyTransform, Transform spriteTransform)
        {
            SKMatrixWrapper entityMatrix 
                = (SKMatrixWrapper)entitiyTransform.GetMatrix<SKMatrixWrapper>();
            SKMatrixWrapper spriteMatrix
                = (SKMatrixWrapper)spriteTransform.GetMatrix<SKMatrixWrapper>();

            SKMatrixWrapper result = (SKMatrixWrapper)entityMatrix.Multiply(spriteMatrix);
            return result.Matrix;
        }
    }
}
