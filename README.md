### Creating a KEYSPACE
```
CREATE KEYSPACE IF NOT EXISTS demo 
  WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 2}
```
