using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using System;

namespace CassandraDemo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Enter node IP: ");
            var ip = Console.ReadLine();
            Console.WriteLine($"Enter password: ");
            var password = Console.ReadLine();
            using (var cluster = Cluster.Builder().WithCredentials("cassandra", password).AddContactPoints(ip).Build())
            using (var session = cluster.Connect("demo")) {
                //Raw CQL
                var rowSets = session.Execute("SELECT * FROM listings");

                MappingConfiguration.Global.Define(
                    new Map<Listing>()
                        .TableName("listings")
                        .PartitionKey(u => u.ListingId)
                        .Column(u => u.ListingId, cm => cm.WithName("listing_id"))
                        .Column(u => u.AverageReview, cm => cm.WithName("average_review"))
                );

                //records<=>entities with linq support
                var listingsTable = new Table<Listing>(session);
                while (true)
                {
                    Console.WriteLine($"Enter an ID: ");
                    var keyStroke = Console.ReadKey();
                    int id;
                    if (keyStroke.KeyChar == 'q') { break; }
                    
                    else if (int.TryParse(keyStroke.KeyChar.ToString(), out id))
                    {
                        var listing = listingsTable.FirstOrDefault(x => x.ListingId == id).Execute();
                        Console.WriteLine($"\n\n\tName: {listing?.Name ?? "N/A"}\n\tCity: {listing?.City ?? "N/A"}\n\tState: {listing?.State ?? "N/A"}");
                    }
                    
                }
                
            }

        }
    }
}