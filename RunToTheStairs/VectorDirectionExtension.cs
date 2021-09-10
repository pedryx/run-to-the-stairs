using System.Numerics;


namespace RunToTheStairs
{
    public static class VectorDirectionExtension
    {
        /// <summary>
        /// Convert direction to vector.
        /// </summary>
        public static Vector2 ToVector(this Direction direction) => direction switch
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

        /// <summary>
        /// Convert vector to direction.
        /// </summary>
        public static Direction ToDirection(this Vector2 vector)
        {
            if (vector == new Vector2(0, -1))
                return Direction.Up;
            else if (vector == new Vector2(-1, 0))
                return Direction.Left;
            else if (vector == new Vector2(0, 1))
                return Direction.Down;
            else if (vector == new Vector2(1, 0))
                return Direction.Right;
            else if (vector == new Vector2(-1, -1))
                return Direction.UpLeft;
            else if (vector == new Vector2(1, -1))
                return Direction.UpRight;
            else if (vector == new Vector2(-1, 1))
                return Direction.DownLeft;
            else if (vector == new Vector2(1, 1))
                return Direction.DownRight;
            else
                return Direction.None;
        }
    }
}
