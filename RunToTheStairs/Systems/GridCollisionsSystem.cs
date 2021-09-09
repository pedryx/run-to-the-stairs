using GameLib;

using RunToTheStairs.Components;

using System.Collections.Generic;
using System.Numerics;


namespace RunToTheStairs.Systems
{
    class GridCollisionsSystem : GameSystem<GridEntity, GridCollider>
    {
        private readonly HashSet<Vector2> staticColliderPositions_ = new();
        private readonly Dictionary<Vector2, GridCollider> staticColliders_ = new();
        private readonly List<GridEntity> dynamicColliderEntities_ = new();
        private readonly List<GridCollider> dynamicColliders_ = new();
        private readonly Game game_;

        private bool collision_;

        public GridCollisionsSystem(Game game)
        {
            game_ = game;
        }

        protected override void UpdateItem(float deltaTime,
            GridEntity gridEntity, GridCollider gridCollider)
        {
            if (gridCollider.IsStatic || !(gridEntity.Moving && gridEntity.CanMove))
                return;

            collision_ = false;
            Vector2 nextPosition = gridEntity.GetNextPosition();

            if (staticColliderPositions_.Contains(nextPosition))
            {
                InvokeCollision(gridCollider, gridEntity, staticColliders_[nextPosition]);
            }
            else
            {
                for (int i = 0; i < dynamicColliders_.Count; i++)
                {
                    if (dynamicColliderEntities_[i].Position == nextPosition)
                    {
                        InvokeCollision(gridCollider, gridEntity, dynamicColliders_[i]);
                        break;
                    }
                }
            }

            gridCollider.CollisionOccured = collision_;
        }

        protected override void ProcessAddedEntity(Entity entity)
        {
            GridCollider collider = entity.Get<GridCollider>();
            GridEntity gridEntity = entity.Get<GridEntity>();

            if (collider.IsStatic)
            {
                staticColliderPositions_.Add(gridEntity.Position);
                staticColliders_.Add(gridEntity.Position, collider);
            }
            else
            {
                dynamicColliderEntities_.Add(gridEntity);
                dynamicColliders_.Add(collider);
            }
        }

        public void UpdatePhysics()
        {
            staticColliderPositions_.Clear();
            dynamicColliders_.Clear();
            dynamicColliderEntities_.Clear();

            foreach (var entity in Entities)
            {
                ProcessAddedEntity(entity);
            }
        }

        private void InvokeCollision(GridCollider collider1, GridEntity entity, 
            GridCollider collider2)
        {
            if (collider1.PreventMovement && collider2.PreventMovement)
                entity.Movement = Direction.None;

            collision_ = true;
            if (collider1.CollisionOccured)
                return;

            TryInvokeScript(collider1);
            TryInvokeScript(collider2);
        }

        private void TryInvokeScript(GridCollider collider)
        {
            if (collider.Script != null)
            {
                if (game_.Scripts.ContainsKey(collider.Script))
                    game_.Scripts[collider.Script].Invoke();
                else
                    Logger.Write($"{nameof(GridCollisionsSystem)}: Cannot find script \"{collider.Script}\".");
            }
        }
    }
}
