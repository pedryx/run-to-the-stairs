using GameLib;

using RunToTheStairs.Components;


namespace RunToTheStairs.Systems
{
    class GridAISystem : GameSystem<GridEntity, GridAI>
    {
        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridAI gridAI)
        {
            gridEntity.Movement = Direction.Right;
        }
    }
}
