using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace GameLib
{
    /// <summary>
    /// Represent a dictionary to which you can add or remove items during iterations.
    /// All adding/removing occur when <see cref="Update"/> is called.
    /// </summary>
    public class IterableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> elemements_ = new();
        /// <summary>
        /// Items to add on next <see cref="Update"/> call.
        /// </summary>
        private readonly List<KeyValuePair<TKey, TValue>> toAdd_ = new();
        /// <summary>
        /// Keys to remove on next <see cref="Update"/> call.
        /// </summary>
        private readonly List<TKey> toRemove_ = new();
        /// <summary>
        /// Determine if dictionary should be cleared on next <see cref="Update"/> ca;;/
        /// </summary>
        private bool clear_ = false;

        /// <summary>
        /// Occur when new item is addded.
        /// </summary>
        public event KeyValuePairEvent<TKey, TValue> OnAdd;
        /// <summary>
        /// Occur when new item is removed.
        /// </summary>
        public event KeyValuePairEvent<TKey, TValue> OnRemove;

        /// <summary>
        /// Get or set item in dictionry. Have undefined behaviour when using set for
        /// adding new items!
        /// </summary>
        public TValue this[TKey key] 
        {
            get => elemements_[key];
            set => elemements_[key] = value;
        }

        public ICollection<TKey> Keys => elemements_.Keys;

        public ICollection<TValue> Values => elemements_.Values;

        public int Count => elemements_.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
            => toAdd_.Add(new KeyValuePair<TKey, TValue>(key, value));

        public void Add(KeyValuePair<TKey, TValue> item)
            => toAdd_.Add(item);

        /// <summary>
        /// Marks dictionary for clear. Clear will occur on next <see cref="Update"/> call.
        /// </summary>
        public void Clear()
            => clear_ = true;

        public bool Contains(KeyValuePair<TKey, TValue> item)
            => (elemements_ as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);

        public bool ContainsKey(TKey key)
            => elemements_.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => (elemements_ as ICollection<KeyValuePair<TKey, TValue>>)
                .CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => elemements_.GetEnumerator();

        /// <returns>Always true.</returns>
        public bool Remove(TKey key)
        {
            toRemove_.Add(key);
            return true;
        }

        /// <returns>Always true.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            toRemove_.Add(item.Key);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
            => elemements_.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator()
            => elemements_.GetEnumerator();

        /// <summary>
        /// Clear dictionary if it should be cleared (including all items which should be
        /// added/removed or firstly removes then adds all items which should be added/removed.
        /// Dont call this method during iterations!
        /// </summary>
        public void Update()
        {
            if (clear_)
            {
                elemements_.Clear();
                toAdd_.Clear();
                toRemove_.Clear();
                clear_ = false;
            }
            else
            {
                foreach (var key in toRemove_)
                {
                    var value = elemements_[key];
                    elemements_.Remove(key);
                    OnRemove?.Invoke(this, 
                        new KeyValuePairEventArgs<TKey, TValue>(key, value));
                }
                toRemove_.Clear();

                foreach (var pair in toAdd_)
                {
                    elemements_.Add(pair.Key, pair.Value);
                    OnAdd?.Invoke(this, 
                        new KeyValuePairEventArgs<TKey, TValue>(pair.Key, pair.Value));
                }
                toAdd_.Clear();
            }
        }
    }
}
