using GameLib;

using RunToTheStairs.WPF.Managers;

using SkiaSharp;

using System.Numerics;


namespace RunToTheStairs.WPF
{
    public class SkiaRenderer : IRenderer
    {
        private SKCanvas canvas_;
        private TextureManager textureManager_;

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

        public void Render(string textureName, Matrix4x4 transform, Rectangle? clip = null)
        {
            SKBitmap bitmap = textureManager_[textureName];
            SKRect destination = canvas_.LocalClipBounds;

            canvas_.SetMatrix(transform.ToSKMatrix());
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
    }
}
