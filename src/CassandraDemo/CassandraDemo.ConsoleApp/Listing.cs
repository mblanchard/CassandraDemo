using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CassandraDemo.ConsoleApp
{
    [DebuggerDisplay("{Name} - {City}")]
    public class Listing
    {
        public string City { get; set; }
        public int ListingId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public double AverageReview { get; set; }
    }
}
