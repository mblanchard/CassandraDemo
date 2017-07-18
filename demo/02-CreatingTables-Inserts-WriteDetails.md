# Creating tables, inserts, the details of a write

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
### Performing an insert
```
INSERT INTO demo.listings (listing_id, name, city, state)
  VALUES (9,'Demo Listing','Chicago','Illinois');
```
Note that not all columns are referenced. No nulls or placeholders will be stored, those columns just won't be captured.

### What's in a write?
- Persisted sequentially to the commit log on disk
- Written to the memtable (in-memory)
- Once memtable threshold is reached, data is flushed to corresponding immutable SSTables
- Periodic compaction reduces all versions of a given row across SSTables and produces one complete row, using the latest timestamp for each column
- Deletes are just writes. The data is marked with a tombstone, and is ignored during compaction.
- Updates are also just writes. The new data is stored alongside the old in memtable/SSTables, and on read the latest record is returned.

### Writing some more sample data from csv
```
COPY demo.listings FROM 'listings.csv' WITH DELIMITER=',' AND HEADER=TRUE;
```
