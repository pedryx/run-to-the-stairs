using System.Drawing;
using System.Numerics;
using System.Xml.Serialization;


namespace GameLib.Components
{
    /// <summary>
    /// Represent a 2D animation for sprite sheets.
    /// 
    /// Animation is form the left to the right and on each line is different action.
    /// </summary>
    public class Animation : IComponent
    {

        /// <summary>
        /// Size of the tile in sprite sheet.
        /// </summary>
        public Vector2 TileSize { get; set; } = new Vector2(64, 64);

        /// <summary>
        /// X coor of current tile.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coor of current tile.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Number of tiles in each row.
        /// </summary>
        public int Width { get; set; } = 9;

        /// <summary>
        /// Number of tiles in eahc column.
        /// </summary>
        public int Height { get; set; } = 4;

        /// <summary>
        /// X coor of first tile.
        /// </summary>
        public int StartX { get; set; } = 1;

        /// <summary>
        /// Y coor of first tile.
        /// </summary>
        public int StartY { get; set; } = 3;

        /// <summary>
        /// Number of frames per second.
        /// </summary>
        public float FPS { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Ellapsed time from last frame switch.
        /// </summary>
        public float EllapsedDelay { get; set; }

        /// <summary>
        /// Determine if animation is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        public Animation()
        {
            X = StartX;
            Y = StartY;
            FPS = (Width - StartX);
        }

        public Rectangle GetClip() => new()
        {
            X = (int)(X * TileSize.X),
            Y = (int)(Y * TileSize.Y),
            Width = (int)TileSize.X,
            Height = (int)TileSize.Y,
        };

    }
}
