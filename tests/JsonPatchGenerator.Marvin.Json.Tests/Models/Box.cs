using System;
using System.Collections.Generic;

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

        public override int GetHashCode() =>
            //HashCode.Combine(Title, Id, Inside);
            throw new NotImplementedException();
    }
}
