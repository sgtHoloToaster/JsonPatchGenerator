using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Models
{
    public class ReadOnlyDictionaryWrapper<TKey, TValue, TReadOnlyValue> : IReadOnlyDictionary<TKey, TReadOnlyValue> where TValue : TReadOnlyValue
    {
        readonly IDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionaryWrapper(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            _dictionary = dictionary;
        }
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public IEnumerable<TKey> Keys => _dictionary.Keys;

        public bool TryGetValue(TKey key, out TReadOnlyValue value)
        {
            var result = _dictionary.TryGetValue(key, out var v);
            value = v;
            return result;
        }

        public IEnumerable<TReadOnlyValue> Values =>
            _dictionary.Values
                .Cast<TReadOnlyValue>()
                .ToList();

        public TReadOnlyValue this[TKey key] =>  _dictionary[key];

        public int Count => _dictionary.Count;

        public IEnumerator<KeyValuePair<TKey, TReadOnlyValue>> GetEnumerator() =>
            _dictionary
                .Select(x => new KeyValuePair<TKey, TReadOnlyValue>(x.Key, x.Value))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
