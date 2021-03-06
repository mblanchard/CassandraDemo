# Authentication/Keyspaces/CAP

### Fetch sample data
```
wget https://raw.githubusercontent.com/mblanchard/CassandraDemo/master/listings.csv
wget https://raw.githubusercontent.com/mblanchard/CassandraDemo/master/listingsbystate.csv
```

### Authenticate/connect to local C\* node using cqlsh
- cqlsh is a CQL shell
```
cqlsh -u cassandra -p [PASSWORD]
```

### Creating a keyspace
- Keyspace ~= database
- Defines the replication strategy
- Usually one per application
```
CREATE KEYSPACE IF NOT EXISTS demo 
  WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 3};
```

### Managing CAP through replication factor and consistency levels
- Cassandra is an AP system out of the box
- In the event of a network interruption, we want
  - a.) Nodes to remain consistent, sacrificing availability (CP)
  - b.) Nodes to remain available, sacrificing consistency (AP)
- If consistency is demanded, replication factor and consistency levels can be modified.
- Read or write consistency level of (replication factor/2 + 1) >> QUORUM
- Read consistency level + write consistency level >= replication factor + 1 >> Consistency

### Questions
- Why would we want to have a replication factor greater than 2?
- In this demo, our cluster has 4 nodes. Any lower, and our replication factor of 3 would not be reasonable. Why is that?
