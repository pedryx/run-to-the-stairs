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
            var player = factory_.CreateGridEntity("player", Vector2.One, 2, true);
            var enemy = factory_.CreateGridEntity("enemy", Vector2.Zero, 1);
            var wall = factory_.CreateWall("wall1", new Vector2(5, 0));

            // todo: generate maze

            return player;
        }

    }
}
