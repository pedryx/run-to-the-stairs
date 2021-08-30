using GameLib;

using Priority_Queue;

using RunToTheStairs.Components;

using System;
using System.Numerics;


namespace RunToTheStairs.Systems
{
    /// <summary>
    /// Represent a system which handles entities in grid.
    /// </summary>
    class GridSystem : GameSystem
    {
        private readonly SimplePriorityQueue<Entity> entitiesQueue_ = new();

        private Entity currentEntity_;
        private float currentPriority_;
        private GridEntity currentGridEntity_;
        private Transform currentTransform_;
        private Vector2 startPos_;

        public Grid Grid { get; private set; }

        /// <summary>
        /// Transition between tiles is interpolated and it takes TurnLength / Entities.Count
        /// seconds for each entity.
        /// </summary>
        public float TurnLength { get; set; } = .5f;

        public GridSystem(Grid grid)
            : base(typeof(Transform), typeof(GridEntity))
        {
            Grid = grid;
        }

        public override void Update(float deltaTime)
        {
            if (Entities.Count == 0)
                return;
            if (currentEntity_ == null)
                GetCurrentEntity();
            if (!currentGridEntity_.Move)
                return;

            if (currentGridEntity_.Move)
            {
                float delay = TurnLength / Entities.Count;
                currentGridEntity_.Ellapsed += deltaTime;

                UpdateDistance(delay);
                if (currentGridEntity_.Ellapsed >= delay)
                {
                    currentGridEntity_.Ellapsed = 0;
                    SetPosition();
                    UpdateQueue();
                }
            }
        }

        private void GetCurrentEntity()
        {
            currentEntity_ = entitiesQueue_.First;
            currentPriority_ = entitiesQueue_.GetPriority(currentEntity_);
            currentGridEntity_ = currentEntity_.Get<GridEntity>();
            currentTransform_ = currentEntity_.Get<Transform>();
            startPos_ = currentTransform_.Position;
            entitiesQueue_.Dequeue();
        }

        private void UpdateDistance(float delay)
        {
            float progress = currentGridEntity_.Ellapsed / delay;
            Vector2 movement = DirectionToVector(currentGridEntity_.Movement);
            Vector2 distance = movement * progress * Grid.TileSize;
            currentTransform_.Position = startPos_ + distance;
        }

        private void SetPosition()
        {
            currentGridEntity_.Position += DirectionToVector(currentGridEntity_.Movement);
            currentTransform_.Position = currentGridEntity_.Position * Grid.TileSize + Grid.Position;
        }

        private void UpdateQueue()
        {
            currentGridEntity_.Move = false;
            entitiesQueue_.Enqueue(currentEntity_, 1 / currentGridEntity_.Speed);

            Entity nextEntity = entitiesQueue_.First;
            float nextPriority = entitiesQueue_.GetPriority(nextEntity);

            if (nextPriority > 0)
            {
                foreach (var entity in entitiesQueue_)
                {
                    if (entity == currentEntity_)
                        continue;

                    float priority = entitiesQueue_.GetPriority(entity) - currentPriority_;
                    entitiesQueue_.UpdatePriority(entity, priority);
                }
            }

            currentEntity_ = null;
        }

        protected override void ProcessAddedEntity(Entity entity)
        {
            GridEntity gridEntity = entity.Get<GridEntity>();
            entitiesQueue_.Enqueue(entity, 1 / gridEntity.Speed);
        }

        protected override void ProcessRemovedEntity(Entity entity)
        {
            if (currentEntity_ == entity)
                currentEntity_ = null;

            entitiesQueue_.Remove(entity);
        }

        private static Vector2 DirectionToVector(Direction direction) => direction switch
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
    }
}
