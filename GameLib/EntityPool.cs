using System.Collections;
using System.Collections.Generic;


namespace GameLib
{
    /// <summary>
    /// Represent a pool for entities.
    /// Pool can be associated to system which then handles components of each entity in pool.
    /// </summary>
    public class EntityPool : IEnumerable<Entity>
    {
        private readonly IterableDictionary<string, Entity> entities_ = new();

        /// <summary>
        /// Occur when entity is added to pool.
        /// </summary>
        public event EntityEventHandler OnAdd;
        /// <summary>
        /// Occur when entity is removed from pool.
        /// </summary>
        public event EntityEventHandler OnRemove;

        public EntityPool()
        {
            entities_.OnAdd += Entities_OnAdd;
            entities_.OnRemove += Entities_OnRemove;
        }

        public Entity this[string name]
        {
            get => entities_[name];
        }

        public bool Contains(string name)
            => entities_.ContainsKey(name);

        public void Add(string name, Entity entity)
            => entities_.Add(name, entity);

        public void Remove(string name)
            => entities_.Remove(name);

        public void Update()
        {
            entities_.Update();
            foreach (var entity in entities_.Values)
            {
                entity.Update();
            }
        }

        public IEnumerator<Entity> GetEnumerator()
            => entities_.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private void Entities_OnAdd(object sender, KeyValuePairEventArgs<string, Entity> e)
        {
            OnAdd?.Invoke(this, new EntityEventArgs(e.Value, e.Key));
        }

        private void Entities_OnRemove(object sender, KeyValuePairEventArgs<string, Entity> e)
        {
            OnRemove?.Invoke(this, new EntityEventArgs(e.Value, e.Key));
        }
    }
}
