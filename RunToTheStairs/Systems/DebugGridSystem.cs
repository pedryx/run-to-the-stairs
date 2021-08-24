using GameLib;
using GameLib.Graphics;
using GameLib.Math;

using System.Numerics;


namespace RunToTheStairs.Systems
{
    class DebugGridSystem : RenderSystem
    {
        private readonly Grid grid_;

        public int Stroke { get; set; } = 1;

        public DebugGridSystem(Camera camera, Grid grid)
            : base(camera)
        {
            grid_ = grid;
        }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            IPrimitivesRenderer primitivesRenderer = renderer.GetPrimitivesRenderer();

            for (int i = 0; i <= grid_.Size.X; i++)
            {
                float x = i * grid_.TileSize.X;

                primitivesRenderer.DrawLine(
                    new Vector2(x, 0),
                    new Vector2(x, GlobalSettings.Resolution.Y),
                    Stroke,
                    Camera.GameTransform.GetMatrix()
                );
            }

            for (int i = 0; i <= grid_.Size.Y; i++)
            {
                float y = i * grid_.TileSize.Y;

                primitivesRenderer.DrawLine(
                    new Vector2(0, y),
                    new Vector2(GlobalSettings.Resolution.X, y),
                    Stroke,
                    Camera.GameTransform.GetMatrix()
                );
            }
        }
    }
}
