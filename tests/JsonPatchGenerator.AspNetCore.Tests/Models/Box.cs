using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.AspNetCore.Tests.Models
{
    public class Box
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public List<Box> Inside { get; set; } = new List<Box>();

        public override bool Equals(object obj) =>
            obj is Box box &&
                   Title == box.Title &&
                   Id == box.Id &&
                   EqualityComparer<List<Box>>.Default.Equals(Inside, box.Inside);

        public override int GetHashCode() => 
            HashCode.Combine(Title, Id, Inside);
    }
}
