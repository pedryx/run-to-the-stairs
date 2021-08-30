using GameLib.Components;

using System;
using System.Linq;
using System.Numerics;
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
        private readonly IterableDictionary<Type, IComponent> components_ = new();

        /// <summary>
        /// Determine if entitiy should be added to default entitiy pool after loaded
        /// from file.
        /// </summary>
        internal bool AddToPool { get; private set; }

        public EntityType Type { get; private set; }

        /// <summary>
        /// Occur when new component is added.
        /// </summary>
        public event ComponentEventHandler OnAdd;
        /// <summary>
        /// Occur when new component is removed.
        /// </summary>
        public event ComponentEventHandler OnRemove;

        public Entity()
            : this(EntityType.Game) { }

        public Entity(EntityType type)
        {
            components_.OnAdd += Components_OnAdd;
            components_.OnRemove += Components_OnRemove;

            Type = type;
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

        public Entity Clone()
        {
            var entity = new Entity();
            foreach (var component in components_.Values)
            {
                entity.Add(CloneMaker.Clone(component));
            }
            entity.Update();

            return entity;
        }

        public Vector2 GetVisualSize()
        {
            if (!Contains<Apperance>())
                return Vector2.Zero;

            if (Contains<Animation>())
            {
                return Get<Animation>().TileSize;
            }
            else
            {
                Apperance apperance = Get<Apperance>();

                if (apperance.Sprites.First().Clip.HasValue)
                {
                    return apperance.Sprites.First().Clip.Value.Size;
                }
                else
                {
                    return GlobalSettings.TextureInfoProvider
                        .GetSize(apperance.Sprites.First().Name);
                }
            }
        }

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

        #region XML
        public XmlSchema GetSchema()
            => null;

        public void ReadXml(XmlReader reader)
        {
            ReadAddToPoolAttribute(reader);
            ReadTypeAttribute(reader);

            reader.Read();
            while (reader.Depth != 0)
            {
                string componentName = reader.Name.ToLower();

                if (!TypeFinder.ComponentTypes.ContainsKey(componentName))
                {
                    Logger.Write($"Could not find component with name {reader.Name}!");
                    reader.Read();
                    continue;
                }

                Type type = TypeFinder.ComponentTypes[componentName];
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

        private void ReadAddToPoolAttribute(XmlReader reader)
        {
            string attributeValue = reader.GetAttribute("AddToPool");

            if (attributeValue == null)
                return;

            if (bool.TryParse(attributeValue, out bool value))
            {
                AddToPool = value;
            }
            else
            {
                Logger.Write($"Value \"{reader.Value}\" on " +
                    $"attribute {reader.Name} cannot be parsed!");
            }
        }

        private void ReadTypeAttribute(XmlReader reader)
        {
            string attributeValue = reader.GetAttribute("Type");

            if (attributeValue == null)
                return;

            if (Enum.TryParse(attributeValue, out EntityType value))
            {
                Type = value;
            }
            else
            {
                Logger.Write($"Value \"{reader.Value}\" on " +
                    $"attribute {reader.Name} cannot be parsed!");
            }
        }
        #endregion
    }
}
