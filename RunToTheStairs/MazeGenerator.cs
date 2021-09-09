using GameLib;

using System;
using System.Collections.Generic;
using System.Numerics;


namespace RunToTheStairs
{
    class MazeGenerator
    {
        private readonly HashSet<Vector2> roomFloorTiles_ = new();
        private readonly HashSet<Vector2> corridorFloorTiles_ = new();
        private readonly HashSet<Vector2> roomWallTiles_ = new();
        private readonly HashSet<Vector2> corridorWallTiles_ = new();
        private readonly List<Rectangle> rooms_ = new();
        private readonly Random random_;
        private readonly Grid grid_;
        private Vector2 stairsTile_;

        public IReadOnlySet<Vector2> RoomFloorTiles => roomFloorTiles_;

        public IReadOnlySet<Vector2> CorridorFloorTiles => corridorFloorTiles_;

        public IReadOnlySet<Vector2> RoomWallTiles => roomWallTiles_;

        public IReadOnlySet<Vector2> CorridorWallTiles => corridorWallTiles_;

        public Vector2 StairsTile => stairsTile_;

        public int MinRoomCount { get; set; }

        public int MaxRoomCount { get; set; }

        public int MinRoomSize { get; set; }

        public int MaxRoomSize { get; set; }

        public MazeGenerator(Grid grid, int? seed = null)
        {
            if (seed.HasValue)
                random_ = new Random(seed.Value);
            else
                random_ = new Random();
            grid_ = grid;
        }

        public void Generate()
        {
            GenerateStairsRoom();
            GenerateRooms();
            GenerateCorridors();
        }

        private void GenerateCorridors()
        {
            for (int i = 1; i < rooms_.Count; i++)
            {
                Rectangle current = rooms_[i];
                Rectangle connected = rooms_[random_.Next(i)];

                GenerateCorridor(current, connected);
            }
        }

        private void GenerateStairsRoom()
        {
            var room = new Rectangle()
            {
                X = random_.Next((int)(grid_.Size.X * .8f), (int)grid_.Size.X),
                Y = random_.Next((int)grid_.Size.Y),
                Width = 3,
                Height = 3,
            };
            rooms_.Add(room);
            GenerateRoom(room);

            stairsTile_ = room.Position + Vector2.One;
        }

        private void GenerateRooms()
        {
            int roomCount = random_.Next(MinRoomCount, MaxRoomCount);
            for (int i = 0; i < roomCount; i++)
            {
                var room = new Rectangle()
                {
                    X = random_.Next((int)grid_.Size.X),
                    Y = random_.Next((int)grid_.Size.Y),
                    Width = random_.Next(MinRoomSize, MaxRoomSize),
                    Height = random_.Next(MinRoomSize, MaxRoomSize),
                };
                rooms_.Add(room);
                GenerateRoom(room);
            }
        }

        private void GenerateRoom(Rectangle room)
        {
            for (int x = (int)room.X; x < (int)(room.X + room.Width); x++)
            {
                for (int y = (int)room.Y; y < (int)(room.Y + room.Height); y++)
                {
                    AddRoomTile(x, y);
                }
            }
        }

        private void GenerateCorridor(Rectangle room1, Rectangle room2)
        {
            Vector2 start = GetRandomWallTile(room1);
            Vector2 end = GetRandomWallTile(room2);
            Vector2 last;

            // first X
            if (random_.Next(2) == 0)
            {
                last = CreateCorridorXAxis(start, end, true);
                last = CreateCorridorYAxis(last, end, false);
                CreateCorridorXAxis(last, end, false);
            }
            // first Y
            else
            {
                last = CreateCorridorYAxis(start, end, true);
                last = CreateCorridorXAxis(last, end, false);
                CreateCorridorYAxis(last, end, false);
            }
        }

        private void AddRoomTile(int x, int y)
            => AddRoomTile(new Vector2(x, y));

        private void AddRoomTile(Vector2 tile)
        {
            if (!roomFloorTiles_.Contains(tile) && !corridorFloorTiles_.Contains(tile))
            {
                roomFloorTiles_.Add(tile);

                if (roomWallTiles_.Contains(tile))
                    roomWallTiles_.Remove(tile);
                if (corridorWallTiles_.Contains(tile))
                    corridorWallTiles_.Remove(tile);

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Vector2 current = tile + new Vector2(x, y);

                        if (
                            !roomFloorTiles_.Contains(current) &&
                            !corridorFloorTiles_.Contains(current) &&
                            !roomWallTiles_.Contains(current) &&
                            !corridorWallTiles_.Contains(current)
                           )
                        {
                            roomWallTiles_.Add(current);
                        }
                    }
                }
            }
        }

        private void AddCorridorTile(Vector2 tile)
        {
            if (!corridorFloorTiles_.Contains(tile) && !roomFloorTiles_.Contains(tile))
            {
                corridorFloorTiles_.Add(tile);

                if (roomWallTiles_.Contains(tile))
                    roomWallTiles_.Remove(tile);
                if (corridorWallTiles_.Contains(tile))
                    corridorWallTiles_.Remove(tile);

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        Vector2 current = tile + new Vector2(x, y);

                        if (
                            !roomFloorTiles_.Contains(current) &&
                            !corridorFloorTiles_.Contains(current) &&
                            !roomWallTiles_.Contains(current) &&
                            !corridorWallTiles_.Contains(current)
                           )
                        {
                            corridorWallTiles_.Add(current);
                        }
                    }
                }
            }
        }

        private Vector2 CreateCorridorXAxis(Vector2 start, Vector2 end, bool half)
        {
            Vector2 current = start;
            var add = new Vector2()
            {
                X = end.X > start.X ? 1 : -1,
                Y = 0,
            };
            int endX = (int)((end.X - start.X) / (half ? 2 : 1) + start.X);

            while (current.X != endX)
            {
                current += add;

                AddCorridorTile(current);
            }

            return current;
        }

        private Vector2 CreateCorridorYAxis(Vector2 start, Vector2 end, bool half)
        {
            Vector2 current = start;
            var add = new Vector2()
            {
                X = 0,
                Y = end.Y > start.Y ? 1 : -1,
            };
            int endY = (int)((end.Y - start.Y) / (half ? 2 : 1) + start.Y);

            while (current.Y != endY)
            {
                current += add;

                AddCorridorTile(current);
            }

            return current;
        }

        private Vector2 GetRandomWallTile(Rectangle room)
        {
            Vector2 position;

            // vertical
            if (random_.Next(2) == 0)
            {
                position.X = random_.Next((int)room.Width) + room.X;
                // top
                if (random_.Next(2) == 0)
                {
                    position.Y = room.Y - 1;
                }
                // down
                else
                {
                    position.Y = room.Y + room.Height;
                }
            }
            // horizontal
            else
            {
                position.Y = random_.Next((int)room.Height) + room.Y;
                // left
                if (random_.Next(2) == 0)
                {
                    position.X = room.X - 1;
                }
                //right 
                else
                {
                    position.X = room.X + room.Width;
                }
            }

            return position;
        }
    }
}
