using GameLib;

using RunToTheStairs.Components;

using System.Numerics;


namespace RunToTheStairs.Systems
{
    /// <summary>
    /// Represent aa system which controls ais movement in grid.
    /// </summary>
    class GridAISystem : GameSystem<GridEntity, GridAI>
    {
        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridAI gridAI)
        {
            if (!gridEntity.Move)
            {
                gridEntity.Movement = GetMove(gridEntity.Position);
                gridEntity.Move = true;
            }
        }

        private Direction GetMove(Vector2 position)
        {
            //todo: path-finding
            return Direction.Right;
        }
    }
}
