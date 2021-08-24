using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace GameLib.Managers.IO
{
    /// <summary>
    /// Represent base class for IO managers.
    /// </summary>
    public abstract class IOManager<T>
    {
        /// <summary>
        /// Contains loaded items.
        /// </summary>
        private readonly Dictionary<string, T> items_ = new();

        /// <summary>
        /// Cotains loaded items.
        /// </summary>
        protected IReadOnlyDictionary<string, T> Items => items_;

        /// <summary>
        /// Item returned when requested item is not found.
        /// </summary>
        public T ErrorItem { get; protected set; }

        /// <summary>
        /// Path used by <see cref="LoadAll"/>.
        /// </summary>
        public string DefaultPath { get; private set; }

        /// <summary>
        /// Get loaded item with specific name.
        /// </summary>
        /// <param name="name">Name of loaded item to get.</param>
        /// <returns>Loaded item of specific name, if item was is not loaded then return <see cref="ErrorItem"/>.</returns>
        public T this[string name]
        {
            get
            {
                // todo: Could behave differently which different culture info!
                string nameLower = name.ToLower();

                if (items_.ContainsKey(nameLower))
                    return items_[nameLower];
                else
                    return ErrorItem;
            }
        }

        /// <summary>
        /// Create new IO manager.
        /// </summary>
        /// <param name="defaultPath">Path used by <see cref="LoadAll"/>.</param>
        /// <param name="errorItem"></param>
        public IOManager(string defaultPath, T errorItem = default)
        {
            DefaultPath = defaultPath;
            ErrorItem = errorItem;
        }

        /// <summary>
        /// Occur after each <see cref="LoadAll(string)"/> call.
        /// </summary>
        protected virtual void LoadedAll() { }

        /// <summary>
        /// Load all items from <see cref="DefaultPath"/>.
        /// </summary>
        public void LoadAll()
            => LoadAll(DefaultPath);

        /// <summary>
        /// Load all items from specific path.
        /// </summary>
        /// <param name="path">Path from which will be items loaded.</param>
        public void LoadAll(string path)
        {
            Console.WriteLine($"{GetType().Name}: Load all.");

            if (!Directory.Exists(path))
            {
                Logger.Write($"{GetType().Name}: Directory \"{path}\" used as" +
                    $" default directory dont exist!");
                return;
            }

            var files = Directory.GetFiles(path, string.Empty, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                string name = file.Split('/', '\\').Last().Split('.').First().ToLower();

                if (name.StartsWith('_'))
                    continue;

                if (items_.ContainsKey(name))
                {
                    Logger.Write($"{GetType().Name}: Item with name \"{name}\" already" +
                        $" loaded!");
                    continue;
                }

                T item;
                try
                {
                    item = Load(name, file);
                }
                catch (Exception ex)
                {
                    Logger.Write($"{GetType().Name}: Error while loading item with name" +
                        $" \"{name}\":\n" + ex.Message);
                    continue;
                }
                items_.Add(name, item);
            }

            LoadedAll();
        }

        /// <summary>
        /// Load item from specific file.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="file">File from which should be item loaded.</param>
        /// <returns>Loaded item.</returns>
        public abstract T Load(string name, string file);

        /// <summary>
        /// Save item to specific file.
        /// </summary>
        /// <param name="item">Item to save,</param>
        /// <param name="file">File to which will be item saved.</param>
        public virtual void Save(T item, string file)
        {
            throw new SaveNotSupportedException();
        }

    }
}
