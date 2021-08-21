using GameLib.Managers.IO;

using System;
using System.Collections.Generic;
using System.Threading;


namespace GameLib
{
    /// <summary>
    /// Represent a game of Entity-Component-System architecture. There are three main
    /// structures in this architecture: entities, components and systems. Components are
    /// simple classes (or stuctures) that contains only data. Entity is a simple collection
    /// of components. Systems are the place where are game logic occur, each system handles
    /// specific types of components and are resposible for updating some part of game logic.
    /// </summary>
    /// <remarks>
    /// For example apperance component describes how should entity appear. Every entity with
    /// wants to have some apperance have apperance component. And there is render system which
    /// has internal collection of all apperance components and it takes all these components
    /// and render a apperance of the entitiy according to them.
    /// </remarks>
    public abstract class Game
    {
        /// <summary>
        /// Date and time of prevous call of <see cref="CalcDeltaTime(float)"/>.
        /// </summary>
        private DateTime last_;
        /// <summary>
        /// Determine if next call of <see cref="Render(float)"/> should be skipped.
        /// </summary>
        private bool skipRender_;
        private IEnumerable<GameSystem> gameSystems_;
        private IEnumerable<RenderSystem> renderSystems_;

        /// <summary>
        /// Default entity manager.
        /// </summary>
        public EntityManager EntityManager { get; private set; }

        /// <summary>
        /// Default entiity pool.
        /// </summary>
        public EntityPool Pool { get; private set; } = new EntityPool();

        public Game()
        {
            EntityManager = new EntityManager(this);
        }

        #region Initialization
        protected abstract IEnumerable<GameSystem> InitializeGameSystems();

        protected abstract IEnumerable<RenderSystem> InitializeRenderSystems();

        /// <summary>
        /// Occur before initializing game and render systems.
        /// </summary>
        protected virtual void PreInitialize() { }

        /// <summary>
        /// Occur after initializing game and render systems.
        /// </summary>
        protected virtual void PostInitialize() { }

        public void Initialize()
        {
            PreInitialize();
            gameSystems_ = InitializeGameSystems();
            renderSystems_ = InitializeRenderSystems();
            TypeFinder.Search();

            EntityManager.LoadAll();
            foreach (var system in gameSystems_)
            {
                system.Associate(Pool);
            }
            foreach (var system in renderSystems_)
            {
                system.Associate(Pool);
            }

            PostInitialize();
        }
        #endregion

        /// <summary>
        /// Calculate time between last and current call of <see cref="CalcDeltaTime(float)"/>
        /// and put current thread to sleep if deltaTime is too small or skip next
        /// <see cref="Render(float)"/> call if is it too big. The first call of this method
        /// returns invalid result!.
        /// </summary>
        /// 
        /// <param name="fps">Target framerate.</param>
        /// 
        /// <returns>
        /// Ellapsed time between last and current call of <see cref="CalcDeltaTime(float)"/>.
        /// If this is a first call of this method than the returned delta time is invalid!
        /// </returns>
        public float CalcDeltaTime(float fps = 60)
        {
            if (fps <= 0)
                throw new ArgumentException("Argument cannot be negative or zero.", nameof(fps));

            DateTime current = DateTime.Now;
            float deltaTime = (float)(current - last_).TotalSeconds;
            float desiredDeltaTime = 1 / fps;

            if (deltaTime < desiredDeltaTime)
            {
                Thread.Sleep((int)((desiredDeltaTime - deltaTime) * 1000));
                current = DateTime.Now;
                deltaTime = (float)(current - last_).TotalSeconds;
            }
            else if (deltaTime > desiredDeltaTime)
                skipRender_ = true;

            last_ = current;
            return deltaTime;
        }

        /// <summary>
        /// Update game logic.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between previous and current frame.</param>
        public void Update(float deltaTime)
        {
            foreach (var system in gameSystems_)
            {
                system.Update(deltaTime);
            }
            Pool.Update();
        }

        /// <summary>
        /// Render game. This call could not always happen see description of
        /// <see cref="CalcDeltaTime(float)"/> for more info.
        /// </summary>
        /// <param name="deltaTime">Time ellapsed between previous and current frame.</param>
        public void Render(float deltaTime, IRenderer renderer)
        {
            if (skipRender_)
            {
                skipRender_ = false;
                return;
            }

            renderer.Clear();
            foreach (var system in renderSystems_)
            {
                system.Render(deltaTime, renderer);
            }
        }

    }
}
