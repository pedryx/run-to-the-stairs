using System.Numerics;


namespace RunToTheStairs
{
    public class Grid
    {

        /// <summary>
        /// Number of tiles in width and height.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        /// Size of one tile.
        /// </summary>
        public Vector2 TileSize { get; private set; }

        /// <summary>
        /// Position of top-left corner.
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Represent a total size of a grid in pixels.
        /// </summary>
        public Vector2 TotalSize
        {
            get => TileSize * Size;
        }

        /// <param name="size">Number of tiles in width and height.</param>
        /// <param name="tileSiae">Size of one tile.</param>
        /// <param name="position">Position of top-left corner.</param>
        public Grid(Vector2 size, Vector2 tileSiae, Vector2 position)
        {
            Size = size;
            TileSize = tileSiae;
            Position = position;
        }

        public static Vector2 DirectionToVector(Direction direction) => direction switch
        {
            Direction.Up => new Vector2(0, -1),
            Direction.Left => new Vector2(-1, 0),
            Direction.Down => new Vector2(0, 1),
            Direction.Right => new Vector2(1, 0),
            Direction.UpLeft => new Vector2(-1, -1),
            Direction.UpRight => new Vector2(1, -1),
            Direction.DownLeft => new Vector2(-1, 1),
            Direction.DownRight => new Vector2(1, 1),
            _ => new Vector2(0, 0),
        };
    }
}
