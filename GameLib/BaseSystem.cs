using System;
using System.Collections.Generic;


namespace GameLib
{
    /// <summary>
    /// Represent a base class for systems.
    /// </summary>
    public abstract class BaseSystem
    {
        private List<Entity> entities_ = new();
        private List<Type> supportedTypes_;

        /// <summary>
        /// Entities containing components which are handled by system.
        /// </summary>
        protected IReadOnlyList<Entity> Entities => entities_;
        /// <summary>
        /// Types of components supported by this system.
        /// </summary>
        protected IReadOnlyList<Type> SupportedTypes => supportedTypes_;

        public BaseSystem(params Type[] supportedTypes)
        {
            supportedTypes_ = new List<Type>(supportedTypes);
        }

        /// <summary>
        /// Associate the pool with system so system will handle all components in pool.
        /// </summary>
        public void Associate(EntityPool pool)
        {
            pool.OnAdd += Pool_OnAdd;
            pool.OnRemove += Pool_OnRemove;

            foreach (var entity in pool)
            {
                entity.OnAdd += Entity_OnAdd;
                entity.OnRemove += Entity_OnRemove;
            }
        }

        /// <summary>
        /// Unassociate the pool with system so system will no longel handle all components in
        /// pool.
        /// </summary>
        public void Unassociate(EntityPool pool)
        {
            pool.OnAdd -= Pool_OnAdd;
            pool.OnRemove -= Pool_OnRemove;

            foreach (var entity in pool)
            {
                entity.OnAdd -= Entity_OnAdd;
                entity.OnRemove -= Entity_OnRemove;
            }
        }

        /// <summary>
        /// Determine if entiity contains supported components.
        /// </summary>
        private bool HasComponents(Entity entity)
        {
            bool contains = true;
            foreach (var type in supportedTypes_)
            {
                if (!entity.Contains(type))
                {
                    contains = false;
                    break;
                }
            }

            return contains;
        }

        private void Pool_OnAdd(object sender, EntityEventArgs e)
        {
            e.Entity.OnAdd += Entity_OnAdd;
            e.Entity.OnRemove += Entity_OnRemove;

            if (HasComponents(e.Entity))
                entities_.Add(e.Entity);
        }

        private void Pool_OnRemove(object sender, EntityEventArgs e)
        {
            e.Entity.OnAdd -= Entity_OnAdd;
            e.Entity.OnRemove -= Entity_OnRemove;

            if (!HasComponents(e.Entity))
                entities_.Remove(e.Entity);
        }

        private void Entity_OnAdd(object sender, ComponentEventArgs e)
        {
            if (!supportedTypes_.Contains(e.Component.GetType()))
                return;

            if (!entities_.Contains(e.Entity) && HasComponents(e.Entity))
                entities_.Add(e.Entity);
        }

        private void Entity_OnRemove(object sender, ComponentEventArgs e)
        {
            if (supportedTypes_.Contains(e.Component.GetType()))
                return;

            if (entities_.Contains(e.Entity))
                entities_.Remove(e.Entity);
        }
    }
}
