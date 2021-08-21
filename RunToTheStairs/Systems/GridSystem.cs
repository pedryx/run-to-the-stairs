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
        /// <summary>
        /// Number of tiles in each row.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Number of tiles in each column.
        /// </summary>
        public int Height { get; private set; }

        public float TileWidth { get; private set; }

        public float TileHeight { get; private set; }

        /// <summary>
        /// Position of grid's top-left corner.
        /// </summary>
        public Vector2 GridPosition { get; private set; }

        /// <param name="width">Number of tiles in each row.</param>
        /// <param name="height">Number of tiles in each column.</param>
        /// <param name="gridPosition">Position of grid's top-left corner.</param>
        public GridSystem(int width, int height, float tileWidth, float tileHeight,
            Vector2 gridPosition)
        {
            Width = width;
            Height = height;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            GridPosition = gridPosition;
        }

        protected override void UpdateItem(float deltaTime,
            Transform transform, GridEntity gridEntity)
        {
            switch (gridEntity.Movement)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    gridEntity.Y--;
                    break;
                case Direction.Left:
                    gridEntity.X--;
                    break;
                case Direction.Down:
                    gridEntity.Y++;
                    break;
                case Direction.Right:
                    gridEntity.X++;
                    break;
            }

            transform.Position = GridPosition + new Vector2()
            {
                X = TileWidth * gridEntity.X,
                Y = TileHeight * gridEntity.Y,
            };
        }
    }
}
