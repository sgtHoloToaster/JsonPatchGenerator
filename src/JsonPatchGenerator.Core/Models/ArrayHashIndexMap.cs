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
        public IReadOnlyDictionary<int, IReadOnlyCollection<int>> Map { get; }

        private IReadOnlyList<int> List => _hashes;

        public int this[int key] => List[key];

        public ArrayHashIndexMap(Array objects, Func<object, int> getHash) : this(objects.Cast<object>().ToList(), getHash) { }

        public ArrayHashIndexMap(IEnumerable<object> objects, Func<object, int> getHash)
        {
            Map = new ReadOnlyDictionaryWrapper<int, HashSet<int>, IReadOnlyCollection<int>>(_map);
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
