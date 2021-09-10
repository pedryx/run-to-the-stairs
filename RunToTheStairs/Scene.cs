using GameLib;
using GameLib.Algorithms;

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
        private readonly Dictionary<Vector2, Entity> floorTiles_ = new();

        private Random random_;

        public IReadOnlyDictionary<Vector2, Entity> FloorTiles => floorTiles_;

        public Graph<Vector2> Graph { get; private set; }

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

            factory_.CreateGridEntity("enemy1", spawn + Vector2.One, 1, false);
            factory_.CreateGridEntity("enemy2", spawn - Vector2.One, 1, false);

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
            SpawnMazeTiles(generator);
            CreateGraph(generator);

            return FloorTiles.Keys;
        }

        private void SpawnMazeTiles(MazeGenerator generator)
        {
            foreach (var tile in generator.RoomFloorTiles)
            {
                floorTiles_[tile] = factory_.CreateSimple
                (
                    "floor",
                    $"roomFloor({tile.X},{tile.Y})",
                    tile
                );
            }
            foreach (var tile in generator.CorridorFloorTiles)
            {
                floorTiles_[tile] = factory_.CreateSimple
                (
                    "floor",
                    $"corridorFloor({tile.X},{tile.Y})",
                    tile
                );
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
        }

        private void CreateGraph(MazeGenerator generator)
        {
            Graph = new Graph<Vector2>();

            foreach (var tile in generator.CorridorFloorTiles)
            {
                CreateNode(generator, tile);
            }
            foreach (var tile in generator.RoomFloorTiles)
            {
                CreateNode(generator, tile);
            }

            if (!Graph.ContainsNode(generator.StairsTile))
                Graph.AddNode(generator.StairsTile);
            Graph.Goal = generator.StairsTile;
            Graph.Dijkstra();
        }

        private void CreateNode(MazeGenerator generator, Vector2 tile)
        {
            if (!Graph.ContainsNode(tile))
                Graph.AddNode(tile);

            CreateEdge(generator, tile, tile + Direction.Up.ToVector());
            CreateEdge(generator, tile, tile + Direction.Left.ToVector());
            CreateEdge(generator, tile, tile + Direction.Down.ToVector());
            CreateEdge(generator, tile, tile + Direction.Right.ToVector());
        }

        private void CreateEdge(MazeGenerator generator, Vector2 tile, Vector2 neighborTile)
        {
            if (generator.CorridorFloorTiles.Contains(neighborTile) ||
                generator.RoomFloorTiles.Contains(neighborTile))
            {
                if (!Graph.ContainsNode(neighborTile))
                    Graph.AddNode(neighborTile);

                Graph.AddEdge(tile, neighborTile, 1);
            }
        }

    }
}
