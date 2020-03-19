using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Models
{
    internal class ArrayHashIndexMap : IEnumerable<int>
    {
        readonly List<int> _hashes = new List<int>();
        readonly Dictionary<int, HashSet<int>> _map = new Dictionary<int, HashSet<int>>();
#if (NET45)
        public IReadOnlyDictionary<int, HashSet<int>> Map { get; } //TODO: replace HashSet with any readonly data type
#else
        public IReadOnlyDictionary<int, IReadOnlyCollection<int>> Map { get; } //TODO: replace IReadOnlyCollection with some readonly HashSet implementation
#endif

        private IReadOnlyList<int> List => _hashes;

        public int this[int key] => List[key];

        public ArrayHashIndexMap(Array objects, Func<object, int> getHash) : this(objects.Cast<object>(), getHash) { }

        public ArrayHashIndexMap(IEnumerable<object> objects, Func<object, int> getHash)
        {
#if (NET45)
            Map = new ReadOnlyDictionaryWrapper<int, HashSet<int>, HashSet<int>>(_map);
#else
            Map = new ReadOnlyDictionaryWrapper<int, HashSet<int>, IReadOnlyCollection<int>>(_map);
#endif
            var hashes = objects.Select(o => getHash(o)).ToList();
            AddRange(hashes);
        }

        public void AddRange(IEnumerable<int> hashes)
        {
            var index = _hashes.Count;
            foreach (var hash in hashes)
                AddHashIndex(hash, index++);

            _hashes.AddRange(hashes);
        }

        public void Add(int hash)
        {
            var index = _hashes.Count;
            _hashes.Add(hash);
            AddHashIndex(hash, index);
        }

        private void AddHashIndex(int hash, int index)
        {
            if (_map.TryGetValue(hash, out var indexes))
                indexes.Add(index);
            else
                _map[hash] = new HashSet<int> { index };
        }

        public IEnumerator<int> GetEnumerator() =>
            List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
