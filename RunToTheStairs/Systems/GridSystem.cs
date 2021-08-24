using GameLib;

using RunToTheStairs.Components;

using System.Numerics;


namespace RunToTheStairs.Systems
{
    /// <summary>
    /// Represent a system which handles entities in grid.
    /// </summary>
    class GridSystem : GameSystem<Transform, GridEntity>
    {
        public Grid Grid { get; private set; }

        public GridSystem(Grid grid)
        {
            Grid = grid;
        }

        protected override void UpdateItem(float deltaTime,
            Transform transform, GridEntity gridEntity)
        {
            gridEntity.Position += gridEntity.Movement switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Down => new Vector2(0, 1),
                Direction.Right => new Vector2(1, 0),
                _ => new Vector2(0, 0),
            };

            transform.Position = Grid.Position + new Vector2()
            {
                X = Grid.TileSize.X * gridEntity.Position.X,
                Y = Grid.TileSize.Y * gridEntity.Position.Y,
            };
        }
    }
}
