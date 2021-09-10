using GameLib.Components;

using System.Numerics;


namespace GameLib.Graphics
{
    public class Camera
    {
        private Transform targetTransform_;
        private Vector2 centerPos_;

        public Entity Target { get; private set; }

        /// <summary>
        /// Determine if camera should follow the target.
        /// </summary>
        public bool Following { get; set; } = true;

        public Transform GameTransform { get; set; } = new Transform();

        public Transform UITransform { get; set; } = new Transform();

        /// <summary>
        /// Set follow target for camera. Target has to have Transform component.
        /// If there is a change made to the one of followings the position of the camera
        /// will be invalideted:
        /// <see cref="Animation.TileSize"/>,
        /// <see cref="Apperance.Sprites"/>,
        /// <see cref="GlobalSettings.Resolution"/>.
        /// </summary>
        public void Follow(Entity entity)
        {
            Target = entity;

            if (entity == null)
            {
                targetTransform_ = null;
                return;
            }

            targetTransform_ = entity.Get<Transform>();
            centerPos_ = GlobalSettings.Resolution / 2 - entity.GetVisualSize() / 2;
        }

        public void Update()
        {
            if (targetTransform_ != null && Following)
                GameTransform.Position = (centerPos_ - targetTransform_.Position * GameTransform.Scale);
        }
    }
}
