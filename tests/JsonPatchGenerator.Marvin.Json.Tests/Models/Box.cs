using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Marvin.Json.Tests.Models
{
    public class Box
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public Box[] Inside { get; set; }

        public override bool Equals(object obj) =>
            obj is Box box &&
                   Title == box.Title &&
                   Id == box.Id &&
                   EqualityComparer<Box[]>.Default.Equals(Inside, box.Inside);

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + (Title?.GetHashCode() ?? 0);
                hash *= 23 + Id.GetHashCode();
                hash *= 23 + (Inside == null ? 13 : 11);
                if (Inside?.Any() ?? false)
                    hash *= 23 + Inside.Aggregate(19, (acc, box) => acc *= 31 + box.GetHashCode());
            }

            return hash;
        }
    }
}
