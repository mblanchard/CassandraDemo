### What is Cassandra?
 - Cassandra (C\*) is a distributed NoSQL/non-relational database
 - Queries are performed using CQL (the Cassandra Query Language)
   - CQL is a bit like SQL without joins or aggregations
   - C\*/CQL also imposes restrictions on sorting/filtering on read
 - Cassandra was developed internally at Facebook in 2008, became Apache project in 2010

### What is it well-suited for?
-	Write-intensive, high volume data (writes can be faster than reads)
-	Ex: Telemetry/event logging, IoT (utility meters, consumer devices, sensor arrays)

### Who uses it?
Some major users are Apple, Cisco, Facebook, Netflix, Rackspace, Reddit, Soundcloud, Twitter, Wikimedia

### What will be covered?
-	Keyspaces, replication, consistency and CAP
-	Creating tables, inserts, details of a C* write
-	Querying, details of a C* read
-	Data Modeling, Chebotko methodology
-	Cassandra C# driver


### Where can I learn more?
 - [Apache docs](http://cassandra.apache.org/doc/latest/)
 - [DataStax docs/training materials](http://docs.datastax.com/en/landing_page/doc/index.html) 
 - [O'Reilly exam prep/certification](http://www.oreilly.com/data/cassandracert)
