using System;


namespace GameLib
{

    /// <summary>
    /// Represent a handler for entitiy releted events of entitiy pool.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void EntityEventHandler(object sender, EntityEventArgs e);

    /// <summary>
    /// Represent an arguments for entitiy releted event of entitiy pool.
    /// </summary>
    public class EntityEventArgs : EventArgs
    {

        public Entity Entity { get; private set; }

        public string Name { get; private set; }

        public EntityEventArgs(Entity entity, string name)
        {
            Entity = entity;
            Name = name;
        }

    }
}
