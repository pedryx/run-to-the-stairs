using System;


namespace GameLib
{
    /// <summary>
    /// Represent an entitiy. Entitiy is a simple collection of components.
    /// </summary>
    public class Entity
    {
        private readonly IterableDictionary<Type, IComponent> components_ = new();

        /// <summary>
        /// Occur when new component is added.
        /// </summary>
        public event ComponentEventHandler OnAdd;
        /// <summary>
        /// Occur when new component is removed.
        /// </summary>
        public event ComponentEventHandler OnRemove;

        public Entity()
        {
            components_.OnAdd += Components_OnAdd;
            components_.OnRemove += Components_OnRemove;
        }

        public T Get<T>()
            => (T)components_[typeof(T)];

        public bool Contains<T>()
            where T : IComponent
            => components_.ContainsKey(typeof(T));

        internal bool Contains(Type type)
            => components_.ContainsKey(type);

        /// <summary>
        /// Add component. Component will be added at the next <see cref="Update"/> call
        /// which happens at the end of the frame.
        /// </summary>
        public void Add<T>(T component)
            where T : IComponent
            => components_.Add(component.GetType(), component);

        /// <summary>
        /// Remove component. Component will be removed at the next <see cref="Update"/> call
        /// which happens at the end of the frame.
        /// </summary>
        public void Remove<T>()
            where T : IComponent
            => components_.Remove(typeof(T));

        /// <summary>
        /// Adds and removes all components.
        /// </summary>
        public void Update()
            => components_.Update();

        private void Components_OnAdd(object sender,
            KeyValuePairEventArgs<Type, IComponent> e)
        {
            OnAdd?.Invoke(this, new ComponentEventArgs(e.Value, this));
        }

        private void Components_OnRemove(object sender,
            KeyValuePairEventArgs<Type, IComponent> e)
        {
            OnRemove?.Invoke(this, new ComponentEventArgs(e.Value, this));
        }
    }
}
