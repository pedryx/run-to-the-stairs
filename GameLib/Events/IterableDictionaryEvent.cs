namespace GameLib
{

    /// <summary>
    /// Represent a handler for keyValuePairs releated events of iterable dictionary.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void KeyValuePairEvent<TKey, TValue>(object sender,
        KeyValuePairEventArgs<TKey, TValue> e);

    /// <summary>
    /// Represent an arguments for keyValuePairs releated events of iterable dictionary.
    /// </summary>
    public class KeyValuePairEventArgs<TKey, TValue>
    {

        public TKey Key { get; private set; }

        public TValue Value { get; private set; }

        public KeyValuePairEventArgs(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

    }
}
