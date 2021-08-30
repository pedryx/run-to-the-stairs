using GameLib.Components;

using System.Linq;
using System.Numerics;

namespace GameLib.Graphics
{
    public class Camera
    {
        private readonly Game game_;

        private Transform targetTransform_;
        private Vector2 centerPos_;

        public Transform GameTransform { get; set; } = new Transform();

        public Transform UITransform { get; set; } = new Transform();

        public Camera(Game game)
        {
            game_ = game;
        }

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
            if (entity == null)
            {
                targetTransform_ = null;
                return;
            }

            targetTransform_ = entity.Get<Transform>();
            centerPos_ = GlobalSettings.Resolution / 2;

            if (entity.Contains<Animation>())
            {
                Animation animation = entity.Get<Animation>();

                centerPos_ -= animation.TileSize / 2;
            }
            else if (entity.Contains<Apperance>())
            {
                Apperance apperance = entity.Get<Apperance>();

                centerPos_ -= game_.TextureInfoProvider.GetSize(apperance.Sprites.First().Name);
            }
        }

        public void Update()
        {
            if (targetTransform_ != null)
                GameTransform.Position = centerPos_ - targetTransform_.Position;
        }
    }
}
