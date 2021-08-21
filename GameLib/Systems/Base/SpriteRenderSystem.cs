using GameLib.Components;

using System.Numerics;


namespace GameLib.Systems.Base
{
    class SpriteRenderSystem : RenderSystem<Transform, Apperance>
    {
        protected override void RenderItem(float deltaTime, IRenderer renderer,
            Transform transform, Apperance apperance)
        {
            foreach (var sprite in apperance.Sprites)
            {
                Matrix4x4 spriteTransform 
                    = transform.GetMatrix() * sprite.Transform.GetMatrix();

                renderer.Render(sprite.Name, spriteTransform, sprite.Clip);
            }
        }
    }
}
