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
            animation.Y = (int)gridEntity.Movement - 1;
        }
    }
}
