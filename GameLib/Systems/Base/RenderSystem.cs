using GameLib.Graphics;

using System;


namespace GameLib
{
    /// <summary>
    /// Represent a base class for game system which are responsible for rendering.
    /// </summary>
    public abstract class RenderSystem : BaseSystem
    {
        protected Camera Camera { get; private set; }

        protected RenderSystem(Camera camera, params Type[] supportedTypes) 
            : base(supportedTypes)
        {
            Camera = camera;
        }

        /// <summary>
        /// Render part of the game.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        public abstract void Render(float deltaTime, IRenderer renderer);
    }

    /// <summary>
    /// Represent a render system which supports one type.
    /// </summary>
    public abstract class RenderSystem<T> : RenderSystem
        where T : IComponent
    {
        protected Entity Entity { get; private set; }

        protected RenderSystem(Camera camera)
            : base(camera, typeof(T)) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            PreRender(deltaTime, renderer);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T component = entity.Get<T>();

                RenderItem(deltaTime, renderer, component);
            }
            PostRender(deltaTime, renderer);
        }

        /// <summary>
        /// Occur before render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Occur after render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Render item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void RenderItem(float deltaTime, IRenderer renderer, T component);
    }

    /// <summary>
    /// Represent a render system which supports two types.
    /// </summary>
    public abstract class RenderSystem<T1, T2> : RenderSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        protected Entity Entity { get; private set; }

        protected RenderSystem(Camera camera)
            : base(camera, typeof(T1), typeof(T2)) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            PreRender(deltaTime, renderer);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T1 component1 = entity.Get<T1>();
                T2 component2 = entity.Get<T2>();

                RenderItem(deltaTime, renderer, component1, component2);
            }
            PostRender(deltaTime, renderer);
        }

        /// <summary>
        /// Occur before render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Occur after render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Render item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void RenderItem(float deltaTime, IRenderer renderer,
            T1 component1, T2 component2);
    }

    /// <summary>
    /// Represent a render system which supports three types.
    /// </summary>
    public abstract class RenderSystem<T1, T2, T3> : RenderSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        protected Entity Entity { get; private set; }

        protected RenderSystem(Camera camera)
            : base(camera, typeof(T1), typeof(T2), typeof(T3)) { }

        public override void Render(float deltaTime, IRenderer renderer)
        {
            PreRender(deltaTime, renderer);
            foreach (var entity in Entities)
            {
                Entity = entity;

                T1 component1 = entity.Get<T1>();
                T2 component2 = entity.Get<T2>();
                T3 component3 = entity.Get<T3>();

                RenderItem(deltaTime, renderer, component1, component2, component3);
            }
            PostRender(deltaTime, renderer);
        }

        /// <summary>
        /// Occur before render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PreRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Occur after render.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected virtual void PostRender(float deltaTime, IRenderer renderer) { }

        /// <summary>
        /// Render item.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between last and current frame.</param>
        protected abstract void RenderItem(float deltaTime, IRenderer renderer,
            T1 component1, T2 component2, T3 component3);
    }
}
