using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GameLib
{
    /// <summary>
    /// Represent an entitiy. Entitiy is a simple collection of components.
    /// </summary>
    public class Entity : IXmlSerializable
    {
        private IterableDictionary<Type, IComponent> components_ = new();

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
        /// Create and add component to entity. Component will be added at the next 
        /// <see cref="Update"/> call which happens at the end of the frame.
        /// </summary>
        public void Add<T>()
            where T : IComponent, new()
            => components_.Add(typeof(T), new T());

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

        public XmlSchema GetSchema()
            => null;

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            while (reader.Depth != 0)
            {
                Type type = TypeFinder.ComponentTypes[reader.Name];
                IComponent component = (IComponent)new XmlSerializer(type).Deserialize(reader);

                if (component == null)
                    break;

                components_.Add(type, component);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var component in components_.Values)
            {
                new XmlSerializer(component.GetType()).Serialize(writer, component);
            }
        }
    }
}
