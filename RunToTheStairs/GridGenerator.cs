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
            int count = 0;
            for (int i = 0; i < grid_.Size.X; i++)
            {
                for (int j = 0; j < grid_.Size.Y; j++)
                {
                    factory_.CreateSimple("floor", "floor" + count, new Vector2(i, j));
                    count++;
                }
            }
            
            var player = factory_.CreateGridEntity("player", Vector2.One, 2, true);
            var enemy = factory_.CreateGridEntity("enemy", Vector2.Zero, 1);
            var wall = factory_.CreateSimple("wall", "wall1", new Vector2(5, 0));

            // todo: generate maze

            return player;
        }

    }
}
