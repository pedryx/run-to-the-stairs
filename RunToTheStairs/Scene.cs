using GameLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace RunToTheStairs
{
    class Scene
    {
        private readonly EntityFactory factory_;
        private readonly Grid grid_;

        private Random random_;

        public Scene(Game game, Grid grid, IApperanceProvider provider)
        {
            factory_ = new EntityFactory(game, grid, provider);
            grid_ = grid;
        }

        public Entity Create(int? seed = null)
        {
            random_ = seed.HasValue ? new Random(seed.Value) : new Random();

            var spawnTiles = SpawnMaze(seed);
            var player = SpawnEntities(spawnTiles);

            return player;
        }

        /// <summary>
        /// Spawn entities in the grid.
        /// </summary>
        /// <returns>Player entity.</returns>
        private Entity SpawnEntities(IEnumerable<Vector2> spawnTiles)
        {
            Vector2 spawn = spawnTiles.ElementAt(random_.Next(spawnTiles.Count()));

            var player = factory_.CreateGridEntity("player", spawn, 2, true);
            _ = factory_.CreateGridEntity("enemy", spawn + Vector2.One, 1);

            return player;
        }

        private IEnumerable<Vector2> SpawnMaze(int? seed = null)
        {
            var generator = new MazeGenerator(grid_, seed)
            {
                MinRoomCount = 60,
                MaxRoomCount = 100,
                MinRoomSize = 4,
                MaxRoomSize = 10,
            };
            generator.Generate();

            foreach (var tile in generator.RoomFloorTiles)
            {
                factory_.CreateSimple("floor", $"roomFloor({tile.X},{tile.Y})", tile);
            }
            foreach (var tile in generator.CorridorFloorTiles)
            {
                factory_.CreateSimple("floor", $"corridorFloor({tile.X},{tile.Y})", tile);
            }

            foreach (var tile in generator.RoomWallTiles)
            {
                factory_.CreateSimple("wall", $"roomWall({tile.X},{tile.Y})", tile);
            }
            foreach (var tile in generator.CorridorWallTiles)
            {
                factory_.CreateSimple("wall", $"corridorWall({tile.X},{tile.Y})", tile);
            }
            factory_.CreateSimple("stairs", "stairs", generator.StairsTile);

            return generator.RoomFloorTiles;
        }

    }
}
