using GameLib;
using GameLib.Graphics;

using System.Numerics;


namespace RunToTheStairs.Systems
{
    public class DebugCameraSystem : RenderSystem
    {
        private Direction direction_;
        private Zoom zoom_;

        public float MoveSpeed { get; set; } = 1000;

        public float ZoomSpeed { get; set; } = 1;

        public DebugCameraSystem(Camera camera)
            : base(camera) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            float distance = MoveSpeed * deltaTime;
            Vector2 deltaPosition = direction_ switch
            {
                Direction.Up => new Vector2(0, distance),
                Direction.Down => new Vector2(0, -distance),
                Direction.Right => new Vector2(-distance, 0),
                Direction.Left => new Vector2(distance, 0),
                _ => new Vector2(0, 0),
            };
            Camera.GameTransform.Position += deltaPosition;

            float zoomAmount = zoom_ switch
            {
                Zoom.In => ZoomSpeed,
                Zoom.Out => -ZoomSpeed,
                _ => 0,
            } * deltaTime;
            Camera.GameTransform.Scale += new Vector2(zoomAmount, zoomAmount);
        }

        public void SetMove(Direction direction)
        {
            direction_ = direction;
        }

        public void SetZoom(Zoom zoom)
        {
            zoom_ = zoom;
        }
    }
}
