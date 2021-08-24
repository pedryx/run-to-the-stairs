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

        public EntityFactory(Game game, Grid grid)
        {
            game_ = game;
            grid_ = grid;
        }

        /// <summary>
        /// Create skeleton.
        /// </summary>
        /// <param name="name">Name of the entiity.</param>
        /// <param name="position">Position in grid.</param>
        /// <returns>Created skeleton entity.</returns>
        public Entity CreateSkeleton(string name, Vector2 position)
        {
            Entity skeleton = game_.EntityManager["skeleton"].Clone();

            var apperance = skeleton.Get<Apperance>();
            var animation = skeleton.Get<Animation>();
            foreach (var sprite in apperance.Sprites)
            {
                Vector2 size = animation.TileSize;

                sprite.Transform.Scale = new Vector2()
                {
                    X = grid_.TileSize.X / size.X,
                    Y = grid_.TileSize.Y / size.Y,
                };
            }

            var gridEntity = skeleton.Get<GridEntity>();
            gridEntity.Position = position;

            game_.Pool.Add(name, skeleton);

            return skeleton;
        }
    }
}