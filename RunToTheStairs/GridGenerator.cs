using GameLib;

using System.Numerics;


namespace RunToTheStairs
{
    class GridGenerator
    {
        private EntityFactory factory_;
        private Grid grid_;

        public GridGenerator(Game game, Grid grid)
        {
            factory_ = new EntityFactory(game, grid);
            grid_ = grid;
        }

        /// <summary>
        /// Spawn entities in the grid.
        /// </summary>
        /// <returns>Player entity.</returns>
        public Entity SpawnEntities()
        {
            var skeletonSlow = factory_.CreateSkeleton("skeletonSlow", new Vector2(0, 0), 1);
            var skeletonFast = factory_.CreateSkeleton("skeletonFast", new Vector2(0, 1), 2);

            return skeletonFast;
        }

    }
}
