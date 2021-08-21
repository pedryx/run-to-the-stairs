using GameLib.Components;


namespace GameLib.Systems
{
    public class SpriteRenderSystem : RenderSystem<Transform, Apperance>
    {
        protected override void PreRender(float deltaTime, IRenderer renderer)
        {
            renderer.StartRender();
        }

        protected override void RenderItem(float deltaTime, IRenderer renderer,
            Transform transform, Apperance apperance)
        {
            foreach (var sprite in apperance.Sprites)
            {
                renderer.Render(sprite, transform, sprite.Clip);
            }
        }

        protected override void PostRender(float deltaTime, IRenderer renderer)
        {
            renderer.EndRender();
        }
    }
}
