### Querying the table
```
SELECT * FROM listings;
```

### What's in a read?
- Check the memtable (recent writes will be captured here)
- Check the row cache, if enabled (hot/frequently accessed rows are cached here)
- On a memtable/cache miss, look at likely target SSTables in the bloom filter
  - Bloom filters are probabilistic, they can only provide potential matches and definite non-matches
- Check the partition key cache for requested partition key, if enabled
  - It will contain the disk-offset of frequently accessed partition keys
- Otherwise, use the partition summary/partition index to find the disk offset for the requested partition key and read sequentially.
- Take all returned columns associated with the partition key, produce a most-recent row in memory (write timestamps) and return row

### Additional querying
```
SELECT * FROM listings WHERE listing_id = 2;
SELECT listing_id FROM listings WHERE state = 'Illinois';
```

### Questions
Why didn't this last query work as expected?
