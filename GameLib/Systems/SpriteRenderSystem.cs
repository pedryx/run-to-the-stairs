using GameLib.Components;
using GameLib.Graphics;
using GameLib.Math;

using System.Drawing;
using System.Numerics;


namespace GameLib.Systems
{
    public class SpriteRenderSystem : RenderSystem<Transform, Apperance>
    {
        public SpriteRenderSystem(Camera camera)
            : base(camera) { }

        protected override void PreRender(float deltaTime, IRenderer renderer)
        {
            renderer.StartRender();
        }

        protected override void RenderItem(float deltaTime, IRenderer renderer,
            Transform transform, Apperance apperance)
        {
            foreach (var sprite in apperance.Sprites)
            {
                IMatrix entityTransform = transform.GetMatrix();
                IMatrix spriteTransform = sprite.Transform.GetMatrix();
                IMatrix cameraTransform = GetCameraTransform();

                IMatrix word = entityTransform.Concat(spriteTransform);
                IMatrix view = cameraTransform.Concat(word);

                if (IsInViewClip(view))
                    renderer.Render(sprite.Name, view, sprite.Color, sprite.Clip);
            }
        }

        protected override void PostRender(float deltaTime, IRenderer renderer)
        {
            renderer.EndRender();
        }

        private IMatrix GetCameraTransform() => Entity.Type switch
        {
            EntityType.Game => Camera.GameTransform.GetMatrix(),
            EntityType.UI => Camera.UITransform.GetMatrix(),
            _ => Matrix.GetIdentity(),
        };

        private bool IsInViewClip(IMatrix view)
        {
            var viewClip = new Rectangle(0, 0,
                (int)GlobalSettings.Resolution.X, (int)GlobalSettings.Resolution.Y);

            Vector2 pos = view.MapPoint(Vector2.Zero);
            Vector2 size = Entity.GetVisualSize();
            Vector2 scale = size * 0f;
            var entityClip = new Rectangle
            (
                (int)(pos.X - scale.X),
                (int)(pos.Y - scale.Y),
                (int)(size.X + 2 * scale.X),
                (int)(size.Y + 2 * scale.Y)
            );

            return viewClip.X < entityClip.X + entityClip.Width &&
                   viewClip.X + viewClip.Width > entityClip.X &&
                   viewClip.Y < entityClip.Y + entityClip.Height &&
                   viewClip.Y + viewClip.Height > entityClip.Y;
        }
    }
}
