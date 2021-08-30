using GameLib;

using System.Numerics;


namespace RunToTheStairs
{
    class GridGenerator
    {
        private readonly EntityFactory factory_;
        private readonly Grid grid_;

        public GridGenerator(Game game, Grid grid, IApperanceProvider provider)
        {
            factory_ = new EntityFactory(game, grid, provider);
            grid_ = grid;
        }

        /// <summary>
        /// Spawn entities in the grid.
        /// </summary>
        /// <returns>Player entity.</returns>
        public Entity SpawnEntities()
        {
            //var skeletonSlow = factory_.CreateGridEntity("skeletonSlow", new Vector2(0, 0), 1);
            var skeletonFast = factory_.CreateGridEntity
            (
                "skeletonFast",
                new Vector2(0, 1),
                2,
                true
            );
            var skeletonSlow = factory_.CreateGridEntity
            (
                "skeletonSlow",
                new Vector2(0, 0),
                1
            );

            return skeletonFast;
        }

    }
}
