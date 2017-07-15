# Demo Script

### Fetch sample data
```
wget https://raw.githubusercontent.com/mblanchard/CassandraDemo/master/listings.csv
```

### Authenticate/connect to local C\* node using cqlsh
```
cqlsh -u cassandra -p [PASSWORD]
```

### Creating a keyspace
- Defines the replication strategy
- Usually one per application
```
CREATE KEYSPACE IF NOT EXISTS demo 
  WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 2}
```
### Creating our tables
```
use demo;
CREATE TABLE IF NOT EXISTS listings (
  listing_id int,
  name text,
  city text,
  state text,
  average_review double,
  description text,  
  PRIMARY KEY (listing_id)
);

CREATE TABLE IF NOT EXISTS listings_by_state (
  state text,
  listing_id int,
  name text,
  city text,
  PRIMARY KEY (state, city)
);
```
### Import sample data from csv
```
COPY demo.listings FROM 'listings.csv' WITH DELIMITER=',' AND HEADER=TRUE;
```
