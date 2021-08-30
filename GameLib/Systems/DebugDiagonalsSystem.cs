using GameLib.Graphics;
using GameLib.Math;

using System.Numerics;


namespace GameLib.Systems
{
    public class DebugDiagonalsSystem : RenderSystem
    {

        public float Stroke { get; set; } = 5;

        public DebugDiagonalsSystem(Camera camera) 
            : base(camera) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            IPrimitivesRenderer primitivesRenderer = renderer.GetPrimitivesRenderer();

            primitivesRenderer.DrawLine(
                Vector2.Zero,
                GlobalSettings.Resolution,
                Stroke,
                Matrix.GetIdentity()
            );
            
            primitivesRenderer.DrawLine(
                new Vector2(GlobalSettings.Resolution.X, 0),
                new Vector2(0, GlobalSettings.Resolution.Y),
                Stroke,
                Matrix.GetIdentity()
            );
        }
    }
}
