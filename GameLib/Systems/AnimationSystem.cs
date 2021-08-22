using GameLib.Components;


namespace GameLib.Systems
{
    /// <summary>
    /// Represent a system for 2D sprite sheets animation.
    /// </summary>
    public class AnimationSystem : GameSystem<Apperance, Animation>
    {
        protected override void UpdateItem(float deltaTime,
            Apperance apperance, Animation animation)
        {
            if (animation.Enabled)
            {
                float delay = 1 / (animation.FPS * GlobalSettings.GameSpeed);

                animation.EllapsedDelay += deltaTime;
                if (animation.EllapsedDelay >= delay)
                {
                    animation.EllapsedDelay -= delay;
                    animation.X++;

                    if (animation.X == animation.Width)
                        animation.X = animation.StartX;
                }
            }

            foreach (var sprite in apperance.Sprites)
            {
                sprite.Clip = animation.GetClip();
            }
        }
    }
}
