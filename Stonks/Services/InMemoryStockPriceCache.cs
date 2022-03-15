using Stonks.Contracts;
using Stonks.Models;

namespace Stonks.Services
{
    public class InMemoryStockPriceCache : ICache<IEnumerable<StockPrice>>
    {
        private readonly Dictionary<string, IEnumerable<StockPrice>> _cache;
        private readonly Queue<string> _keys;
        private readonly int _capacity;

        public InMemoryStockPriceCache(int capacity = 1000)
        {
            _capacity = capacity;
            _cache = new Dictionary<string, IEnumerable<StockPrice>>(capacity);
            _keys = new Queue<string>(capacity);
        }

        public bool TryGetValue(string ticker, out IEnumerable<StockPrice> cachedValue)
        {
            return _cache.TryGetValue(ticker, out cachedValue);
        }

        public void AddOrReplace(string ticker, IEnumerable<StockPrice> valueToCache)
        {
            if (valueToCache == null)
            {
                return;
            }

            if (_cache.ContainsKey(ticker))
            {
                _cache[ticker] = valueToCache;
            }
            else
            {
                MaintainCapacity();

                _cache.Add(ticker, valueToCache);
                _keys.Enqueue(ticker);
            }
        }

        private void MaintainCapacity()
        {
            if (_cache.Count == _capacity)
            {
                string oldestKey = _keys.Dequeue();
                _cache.Remove(oldestKey);
            }
        }
    }
}
