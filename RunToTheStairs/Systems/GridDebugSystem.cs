using GameLib;

using System.Numerics;


namespace RunToTheStairs.Systems
{
    class GridDebugSystem : RenderSystem
    {
        private readonly Grid grid_;
        private readonly Game game_;

        public int Stroke { get; set; } = 1;

        public GridDebugSystem(Game game, Grid grid)
        {
            grid_ = grid;
            game_ = game;
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
                    game_.MathProvider.GetIdentityMatrix()
                );
            }

            for (int i = 0; i <= grid_.Size.Y; i++)
            {
                float y = i * grid_.TileSize.Y;

                primitivesRenderer.DrawLine(
                    new Vector2(0, y),
                    new Vector2(GlobalSettings.Resolution.X, y),
                    Stroke,
                    game_.MathProvider.GetIdentityMatrix()
                );
            }
        }
    }
}
