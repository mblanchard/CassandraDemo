# Demo Script

### Creating a keyspace
- Defines the replication strategy
- Usually one per application
```
CREATE KEYSPACE IF NOT EXISTS demo 
  WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 2}
```
### Creating a table
```
using demo;
CREATE TABLE IF NOT EXISTS listings (
  name text,
  state text,
  city text
  PRIMARY KEY (name, state)
);
```
