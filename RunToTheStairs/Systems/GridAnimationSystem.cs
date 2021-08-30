using GameLib;
using GameLib.Components;

using RunToTheStairs.Components;


namespace RunToTheStairs.Systems
{
    class GridAnimationSystem : GameSystem<GridEntity, Animation>
    {
        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, Animation animation)
        {
            animation.Y = gridEntity.Movement switch
            {
                Direction.Up => 0,
                Direction.Left => 1,
                Direction.Down => 2,
                Direction.Right => 3,
                Direction.UpLeft => 0,
                Direction.UpRight => 0,
                Direction.DownLeft => 2,
                Direction.DownRight => 2,
                _ => animation.Y,
            };
        }
    }
}
