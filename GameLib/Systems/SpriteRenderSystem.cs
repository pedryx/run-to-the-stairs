using GameLib.Components;
using GameLib.Graphics;
using GameLib.Math;


namespace GameLib.Systems
{
    public class SpriteRenderSystem : RenderSystem<Transform, Apperance>
    {
        private readonly Game game_;

        public SpriteRenderSystem(Game game)
        {
            game_ = game;
        }

        protected override void PreRender(float deltaTime, IRenderer renderer)
        {
            renderer.StartRender();
        }

        protected override void RenderItem(float deltaTime, IRenderer renderer,
            Transform transform, Apperance apperance)
        {
            foreach (var sprite in apperance.Sprites)
            {
                IMatrix entityTransform = game_.MathProvider.MatrixFromTransform(transform);
                IMatrix spriteTransform = game_.MathProvider.MatrixFromTransform(sprite.Transform);

                IMatrix word = game_.MathProvider.Concat(entityTransform, spriteTransform);
                //todo: view matrix

                renderer.Render(sprite.Name, word, sprite.Clip);
            }
        }

        protected override void PostRender(float deltaTime, IRenderer renderer)
        {
            renderer.EndRender();
        }
    }
}
