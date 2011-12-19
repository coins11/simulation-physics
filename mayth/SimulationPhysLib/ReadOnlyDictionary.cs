using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace SimulationPhysLib
{
    /// <summary>
    /// Represents a readonly dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of key of dictionary</typeparam>
    /// <typeparam name="TValue">Type of value of dictionary</typeparam>
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// Initializes a readonly dictionary that has the items of the specified dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null);
            _dictionary = dictionary;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether a dictionary contains a specified key.
        /// </summary>
        /// <param name="key">The value to search.</param>
        /// <returns>true if this dictionary contains an item that has the specified key; otherwise, false.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the collection of the keys of this dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public bool Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Try to get the value of the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified key if the key is found; otherwise, the default value for the type of <typeparamref name="TValue"/> parameter.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if this dictionary contains the specified key; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Ensures(Contract.Result<bool>() == this.ContainsKey(key));
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Get the collection of the values of this dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether this dictionary contains the specified key-value pair.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Copies the elements of this dictionary to an array of type <see cref="KeyValuePair(Of Tkey, TValue)"/>, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array of type <see cref="KeyValuePair(Of TKey, TValue)"/> that is the destination of the <see cref="KeyValuePair(Of TKey, TValue)"/> elements copied from this dictionary.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the count of the items of this dictionary.
        /// </summary>
        public int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Gets whether this dictionary is readonly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>A <see cref="IEnumerator(Of KeyValuePair(Of TKey, TValue))"/> for this dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>A <see cref="IEnumerator"/> structure for this dictionary.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
