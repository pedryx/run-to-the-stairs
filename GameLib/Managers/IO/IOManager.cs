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
        private Dictionary<string, T> items_ = new();

        /// <summary>
        /// Cotains loaded items.
        /// </summary>
        protected IReadOnlyDictionary<string, T> Items => items_;

        /// <summary>
        /// Item returned when requested item is not found.
        /// </summary>
        protected T ErrorItem { get; private set; }

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
                if (items_.ContainsKey(name))
                    return items_[name];
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
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Directory {path} used as default path for {GetType().Name} dont exist!");
                return;
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                string name = file.Split('/', '\\').Last().Split('.').First();
                T item;
                try
                {
                    item = Load(name, file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                items_.Add(name, item);
            }
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
