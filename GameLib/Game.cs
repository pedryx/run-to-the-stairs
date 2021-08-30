using GameLib.Graphics;
using GameLib.Managers.IO;
using GameLib.Math;

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
        private readonly IterableDictionary<Type, GameSystem> gameSystems_ = new();
        private readonly IterableDictionary<Type, RenderSystem> renderSystems_ = new();

        /// <summary>
        /// Date and time of prevous call of <see cref="CalcDeltaTime(float)"/>.
        /// </summary>
        private DateTime last_;
        /// <summary>
        /// Determine if next call of <see cref="Render(float)"/> should be skipped.
        /// </summary>
        private bool skipRender_;

        /// <summary>
        /// Default entity manager.
        /// </summary>
        public EntityManager EntityManager { get; private set; }

        /// <summary>
        /// Default entiity pool.
        /// </summary>
        public EntityPool Pool { get; private set; } = new EntityPool();

        public Camera Camera { get; private set; }

        public ITextureInfoProvider TextureInfoProvider { get; private set; }

        public Game(ITextureInfoProvider textureInfoProvider, IMathProvider mathProvider)
        {
            Camera = new Camera(this);
            EntityManager = new EntityManager(this);

            TextureInfoProvider = textureInfoProvider;
            GlobalSettings.MathProvider = mathProvider;
        }

        #region Initialization and system methods
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
            // pre initialization phase
            PreInitialize();

            // initialization phase
            foreach (var system in InitializeGameSystems())
            {
                gameSystems_.Add(system.GetType(), system);
            }
            foreach (var system in InitializeRenderSystems())
            {
                renderSystems_.Add(system.GetType(), system);
            }
            TypeFinder.Search();
            EntityManager.LoadAll();

            // post initialization phase
            PostInitialize();

            gameSystems_.OnAdd += GameSystems_OnAdd;
            gameSystems_.OnRemove += GameSystems_OnRemove;
            renderSystems_.OnAdd += RenderSystems_OnAdd;
            renderSystems_.OnRemove += RenderSystems_OnRemove;

            Pool.Update();
            gameSystems_.Update();
            renderSystems_.Update();

            Logger.Write("Game initialized.");
        }

        private void GameSystems_OnAdd(object sender, KeyValuePairEventArgs<Type, GameSystem> e)
            => e.Value.Associate(Pool);

        private void GameSystems_OnRemove(object sender, KeyValuePairEventArgs<Type, GameSystem> e)
            => e.Value.Unassociate(Pool);

        private void RenderSystems_OnAdd(object sender, KeyValuePairEventArgs<Type, RenderSystem> e)
            => e.Value.Associate(Pool);

        private void RenderSystems_OnRemove(object sender, KeyValuePairEventArgs<Type, RenderSystem> e)
            => e.Value.Unassociate(Pool);

        public void AddGameSystem<T>(T system)
            where T : GameSystem
            => gameSystems_.Add(typeof(T), system);

        public void RemoveGameSystem<T>()
            where T : GameSystem
            => gameSystems_.Remove(typeof(T));

        public void AddRenderSystem<T>(T system)
            where T : RenderSystem
            => renderSystems_.Add(typeof(T), system);

        public void RemoveRenderSystem<T>()
            where T : RenderSystem
            => renderSystems_.Remove(typeof(T));
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
            foreach (var system in gameSystems_.Values)
            {
                system.Update(deltaTime);
            }

            gameSystems_.Update();
            Pool.Update();
            Camera.Update();
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
            foreach (var system in renderSystems_.Values)
            {
                system.Render(deltaTime, renderer);
            }

            renderSystems_.Update();
        }

    }
}
