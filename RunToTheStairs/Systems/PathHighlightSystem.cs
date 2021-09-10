using GameLib;
using GameLib.Algorithms;
using GameLib.Components;

using RunToTheStairs.Components;

using System.Collections.Generic;
using System.Drawing;
using System.Numerics;


namespace RunToTheStairs.Systems
{
    /// <summary>
    /// Represent a system which highlight path from the position of a entity with
    /// <see cref="GridPathHighlighter"/> component to the goal of the graph. This system
    /// supports only one entity with <see cref="GridPathHighlighter"/> component!
    /// </summary>
    class PathHighlightSystem : GameSystem<GridPathHighlighter, GridEntity>
    {
        private readonly Scene scene_;

        /// <summary>
        /// Highlight color.
        /// </summary>
        public Color Color { get; set; } = Color.FromArgb(255, 0, 0);

        public PathHighlightSystem(Scene scene)
        {
            scene_ = scene;
        }

        protected override void UpdateItem(float deltaTime,
            GridPathHighlighter gridPathHighlighter, GridEntity gridEntity)
        {
            if (gridEntity.Position == gridPathHighlighter.LastPosition)
                return;

            CancelHighlight(gridPathHighlighter);
            gridPathHighlighter.LastPosition = gridEntity.Position;
            CreateHighlight(gridPathHighlighter);
        }

        private static void CancelHighlight(GridPathHighlighter gridPathHighlighter)
        {
            foreach (var entity in gridPathHighlighter.CurrentHighlight)
            {
                var apperance = entity.Get<Apperance>();
                foreach (var sprite in apperance.Sprites)
                {
                    sprite.Color = Color.White;
                }
            }
            gridPathHighlighter.CurrentHighlight = new List<Entity>();
        }

        private void CreateHighlight(GridPathHighlighter gridPathHighlighter)
        {
            IList<Vector2> path = scene_.Graph.GetPath(gridPathHighlighter.LastPosition);
            foreach (var position in path)
            {
                var floorEntity = scene_.FloorTiles[position];

                HighlightTile(floorEntity);
                gridPathHighlighter.CurrentHighlight.Add(floorEntity);
            }
        }

        private void HighlightTile(Entity entity)
        {
            var apperance = entity.Get<Apperance>();
            foreach (var sprite in apperance.Sprites)
            {
                sprite.Color = Color;
            }
        }

    }
}
