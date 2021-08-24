using GameLib.Graphics;
using GameLib.Math;

using System.Numerics;


namespace GameLib.Systems
{
    public class DebugDiagonalSystem : RenderSystem
    {
        public DebugDiagonalSystem(Camera camera) 
            : base(camera) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            IPrimitivesRenderer primitivesRenderer = renderer.GetPrimitivesRenderer();

            primitivesRenderer.DrawLine(
                Vector2.Zero,
                GlobalSettings.Resolution,
                1,
                Matrix.GetIdentity()
            );
            
            primitivesRenderer.DrawLine(
                new Vector2(GlobalSettings.Resolution.X, 0),
                new Vector2(0, GlobalSettings.Resolution.Y),
                1,
                Matrix.GetIdentity()
            );
        }
    }
}
