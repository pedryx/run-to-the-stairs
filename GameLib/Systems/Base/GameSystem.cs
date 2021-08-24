using System;


namespace GameLib
{
    /// <summary>
    /// Represent a base class for a game systems which are responsible for updating game
    /// logic.
    /// </summary>
    public abstract class GameSystem : BaseSystem
    {
        protected GameSystem(params Type[] supportedTypes) 
            : base(supportedTypes) { }

        /// <summary>
        /// Update part of the game logic.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        public abstract void Update(float deltaTime);
    }

    /// <summary>
    /// Represent a game system which supports one type.
    /// </summary>
    public abstract class GameSystem<T> : GameSystem
        where T : IComponent
    {
        protected Entity Entity { get; private set; }

        protected GameSystem() 
            : base(typeof(T)) { }

        public override void Update(float deltaTime)
        {
            PreUpdate(deltaTime);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T component = entity.Get<T>();

                UpdateItem(deltaTime, component);
            }
        }

        /// <summary>
        /// Occur before update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreUpdate(float deltaTime) { }

        /// <summary>
        /// Occur after update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostUpdate(float deltaTime) { }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void UpdateItem(float deltaTime, T component);
    }

    /// <summary>
    /// Represent a game system which supports two types.
    /// </summary>
    public abstract class GameSystem<T1, T2> : GameSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        protected Entity Entity { get; private set; }

        protected GameSystem() 
            : base(typeof(T1), typeof(T2)) { }

        public override void Update(float deltaTime)
        {
            PreUpdate(deltaTime);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T1 component1 = entity.Get<T1>();
                T2 component2 = entity.Get<T2>();

                UpdateItem(deltaTime, component1, component2);
            }
        }

        /// <summary>
        /// Occur before update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreUpdate(float deltaTime) { }

        /// <summary>
        /// Occur after update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostUpdate(float deltaTime) { }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void UpdateItem(float deltaTime, T1 component1, T2 component2);
    }

    /// <summary>
    /// Represent a game system which supports three types.
    /// </summary>
    public abstract class GameSystem<T1, T2, T3> : GameSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        protected Entity Entity { get; private set; }

        protected GameSystem()
            : base(typeof(T1), typeof(T2), typeof(T3)) { }

        public override void Update(float deltaTime)
        {
            PreUpdate(deltaTime);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T1 component1 = entity.Get<T1>();
                T2 component2 = entity.Get<T2>();
                T3 component3 = entity.Get<T3>();

                UpdateItem(deltaTime, component1, component2, component3);
            }
        }

        /// <summary>
        /// Occur before update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreUpdate(float deltaTime) { }

        /// <summary>
        /// Occur after update.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostUpdate(float deltaTime) { }

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void UpdateItem(float deltaTime, T1 component1, T2 component2,
            T3 component3);
    }
}
