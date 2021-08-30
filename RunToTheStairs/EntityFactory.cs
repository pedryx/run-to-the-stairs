using GameLib;
using GameLib.Components;

using RunToTheStairs.Components;

using System.Numerics;


namespace RunToTheStairs
{
    /// <summary>
    /// Represent a factory for entities.
    /// </summary>
    class EntityFactory
    {
        private readonly Game game_;
        private readonly Grid grid_;
        private readonly IApperanceProvider provider_;

        public EntityFactory(Game game, Grid grid, IApperanceProvider provider)
        {
            game_ = game;
            grid_ = grid;
            provider_ = provider;
        }

        public Entity CreateWall(string name, Vector2 position)
        {
            Entity entity = game_.EntityManager["wall"].Clone();

            PutInGrid(entity, position);

            entity.Update();
            game_.Pool.Add(name, entity);

            return entity;
        }

        /// <summary>
        /// Create skeleton.
        /// </summary>
        /// <param name="name">Name of the entiity.</param>
        /// <param name="position">Position in grid.</param>
        /// <returns>Created skeleton entity.</returns>
        public Entity CreateGridEntity(string name, Vector2 position,
            float speed, bool player = false)
        {
            Entity entity = game_.EntityManager["gridEntity"].Clone();

            var apperance = provider_.GetEntityApperance();
            entity.Add(apperance);

            var gridEntity = entity.Get<GridEntity>();
            gridEntity.Speed = speed;


            if (player)
                entity.Add<GridPlayer>();
            else
                entity.Add<GridAI>();

            entity.Update();
            PutInGrid(entity, position);

            entity.Update();
            game_.Pool.Add(name, entity);

            return entity;
        }

        private void PutInGrid(Entity entity, Vector2 position)
        {
            GridEntity gridEntity = entity.Get<GridEntity>();
            gridEntity.Position = position;
            
            Apperance apperance = entity.Get<Apperance>();
            foreach (var sprite in apperance.Sprites)
            {
                sprite.Transform.Scale = grid_.TileSize / entity.GetVisualSize();
            }

            Transform transform = entity.Get<Transform>();
            transform.Position = grid_.Position + grid_.TileSize * position;
        }

    }
}