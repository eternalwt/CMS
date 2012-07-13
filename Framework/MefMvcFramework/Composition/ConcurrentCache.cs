namespace MefMvcFramework.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a thread-safe cache of objects.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The value for the specified key.</typeparam>
    internal class ConcurrentCache<TKey, TValue>
    {
        #region Fields
        private readonly Dictionary<TKey, TValue> Cache = new Dictionary<TKey, TValue>();
        private readonly object Sync = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the set of cached values.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                lock (Sync)
                    return Cache.Values.ToArray();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fetches the value with the specified key.  If the value does not exist, create it.
        /// </summary>
        /// <param name="key">The key of the value.</param>
        /// <param name="creator">The delegate used to create the instance.</param>
        /// <returns>The value with the specified key.  If the value does not exist, create it.</returns>
        public TValue Fetch(TKey key, Func<TValue> creator)
        {
            lock (Sync)
            {
                TValue value;
                if (!Cache.TryGetValue(key, out value))
                    Cache[key] = value = creator();

                return value;
            }
        }
        #endregion
    }
}