using GameLib.Components;
using GameLib.Graphics;
using GameLib.Math;


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
                IMatrix view = word.Concat(cameraTransform);

                renderer.Render(sprite.Name, view, sprite.Clip);
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
    }
}
