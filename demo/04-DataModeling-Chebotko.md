### Denormalization and data modeling with access patterns (Chebotko methodology)
- Writes are cheap, "Write the data you want to read"
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

### Only two Illinois records, what went wrong here?
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
