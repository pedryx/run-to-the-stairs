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
        private readonly Scene scene_;

        public GridAISystem(Scene scene)
        {
            scene_ = scene;
        }

        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridAI gridAI)
        {
            if (!gridEntity.CanMove)
            {
                gridEntity.Movement = GetMove(gridEntity.Position);
                gridEntity.CanMove = true;
            }
        }

        private Direction GetMove(Vector2 position)
            => (scene_.Graph.GetNext(position) - position).ToDirection();
    }
}
