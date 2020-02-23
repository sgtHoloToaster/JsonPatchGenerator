using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static int GetElementsHashCode<T>(this T[] array) =>
            GetElementsHashCode(array as IEnumerable<T>);

        public static int GetElementsHashCode<T>(this IEnumerable<T> enumerable) =>
            enumerable.Aggregate(13, (total, next) => total *= 17 + (next?.GetHashCode() ?? int.MinValue));
    }
}
