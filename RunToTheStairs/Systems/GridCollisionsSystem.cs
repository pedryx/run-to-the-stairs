using GameLib;

using RunToTheStairs.Components;

using System.Collections.Generic;
using System.Numerics;


namespace RunToTheStairs.Systems
{
    class GridCollisionsSystem : GameSystem<GridEntity, GridCollider>
    {
        private readonly HashSet<Vector2> staticColliders_ = new();
        private readonly List<GridEntity> dynamicColliders_ = new();

        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridCollider gridCollider)
        {
            if (gridCollider.IsStatic || !(gridEntity.Moving && gridEntity.CanMove))
                return;

            Vector2 nextPosition = gridEntity.GetNextPosition();

            if (staticColliders_.Contains(nextPosition))
            {
                gridEntity.Movement = Direction.None;
            }
            else
            {
                foreach (var collider in dynamicColliders_)
                {
                    if (collider.Position == nextPosition)
                    {
                        gridEntity.Movement = Direction.None;
                        break;
                    }
                }
            }
        }

        protected override void ProcessAddedEntity(Entity entity)
        {
            GridCollider collider = entity.Get<GridCollider>();
            GridEntity gridEntity = entity.Get<GridEntity>();

            if (collider.IsStatic)
                staticColliders_.Add(gridEntity.Position);
            else
                dynamicColliders_.Add(gridEntity);
        }

        public void UpdatePhysics()
        {
            staticColliders_.Clear();
            dynamicColliders_.Clear();

            foreach (var entity in Entities)
            {
                ProcessAddedEntity(entity);
            }
        }
    }
}
