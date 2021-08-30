using GameLib;

using RunToTheStairs.Components;


namespace RunToTheStairs.Systems
{
    /// <summary>
    /// Represent a system which controls player movement in grid.
    /// </summary>
    class GridPlayerSystem : GameSystem<GridEntity, GridPlayer>
    {
        /// <summary>
        /// Direction of player's next movement.
        /// </summary>
        private Direction direction_;
        /// <summary>
        /// Determine if player should move during next iteration.
        /// </summary>
        private bool move_;

        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridPlayer gridPlayer)
        {
            if (move_)
            {
                gridEntity.Movement = direction_;
                gridEntity.Move = true;
                move_ = false;
            }
        }

        public void SetMove(Direction direction)
        {
            direction_ = direction;
            move_ = true;
        }
    }
}
