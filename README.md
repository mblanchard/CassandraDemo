# Demo Script

### Fetch sample data
```
wget https://raw.githubusercontent.com/mblanchard/CassandraDemo/master/listings.csv
wget https://raw.githubusercontent.com/mblanchard/CassandraDemo/master/listingsbystate.csv
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
  WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 3}
```

### Managing CAP through replication factor and consistency levels
- Cassandra is an AP system out of the box
- In the event of a network interruption, we want
  - a.) Nodes to remain consistent, sacrificing availability (CP)
  - b.) Nodes to remain available, sacrificing consistency (AP)
- If consistency is demanded, replication factor and consistency levels can be modified.
- Read or write consistency level of (replication factor/2 + 1) >> QUORUM
- Read consistency level + write consistency level >= replication factor + 1 >> Consistency
- Why would we want to avoid a replication factor of 2?

### Creating a listings table
```
use demo;
CREATE TABLE IF NOT EXISTS listings (
  listing_id int,
  average_review double,
  city text,
  description text,
  name text,  
  state text,   
  PRIMARY KEY (listing_id)
);
```
### Performing a write
INSERT INTO demo.listings (listing_id, name, city, state)
  VALUES (9,"
  USING option AND option

### Writing some more sample data from csv
```
COPY demo.listings FROM 'listings.csv' WITH DELIMITER=',' AND HEADER=TRUE;
```

### Querying the table
```
SELECT * FROM listings;
SELECT * FROM listings WHERE listing_id = 2;
SELECT listing_id FROM listings WHERE state = 'Illinois';
```
Why didn't this last query work as expected?

### Denormalization and data modeling with access patterns (Chebotko methodology)
- Sorting/clustering in C\* happens on writes only
- Reads are always sequential and do not allow for filtering (beyond defined partition keys and clustering columns)
- Joins are not possible (at least not in C\* alone, Apache Spark can be used to provide client-side joins)
- Deviating from relational data modeling, in C\* data is typically denormalized on write, rather than stored as a normalized/canonical model and denormalized/joined on read
```
CREATE TABLE IF NOT EXISTS listings_by_state (
  state text,
  city text,
  listing_id int,
  name text, 
  PRIMARY KEY (state, city)
);
```
### Import sample data from csv
```
COPY demo.listings_by_state FROM 'listingsbystate.csv' WITH DELIMITER=',' AND HEADER=TRUE;
```

### What's in the denormalized table?
```
SELECT * FROM listings_by_state WHERE state = 'Illinois';
```

### Only two Chicago records, what went wrong here?
- Clustering columns dictate how data is sorted on disk during a write
- We can't alter the partition keys or clustering columns that make up a primary key in C\*, so we'll need to recreate the table. Fortunately, the content of the table already exists in listingsbystate.csv

### Creating listings_by_state a second time
```
DROP TABLE listings_by_state;
CREATE TABLE listings_by_state (
  state text,
  city text,
  listing_id int,
  name text, 
  PRIMARY KEY (state, city, listing_id)
);
COPY demo.listings_by_state FROM 'listingsbystate.csv' WITH DELIMITER=',' AND HEADER=TRUE;

SELECT * FROM listings_by_state WHERE state = 'Illinois';
```
### The 
