using AutoMapper;
using Stonks.Contracts;
using Stonks.Models;

namespace Stonks.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly ILogger<StockPriceService> _logger;
        private readonly IStockDataProvider _stockDataProvider;
        private readonly ICache<IEnumerable<StockPrice>> _cache;
        private readonly IMapper _mapper;

        public StockPriceService(
            ILogger<StockPriceService> logger,
            IStockDataProvider stockDataProvider,
            ICache<IEnumerable<StockPrice>> cache,
            IMapper mapper)
        {
            _logger = logger;
            _stockDataProvider = stockDataProvider;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<Either<IEnumerable<StockPrice>, Exception>> GetStockPricesByTicker(string ticker)
        {
            if (this._cache.TryGetValue(ticker, out var cachedStockPrices))
            {
                return new Either<IEnumerable<StockPrice>, Exception>(cachedStockPrices);
            }

            var aggregateDataOrException = await this._stockDataProvider.GetAggregateDataByTicker(ticker);

            return aggregateDataOrException.Match((aggregateData) =>
                {
                    var stockPrices = _mapper.Map<List<StockPrice>>(aggregateData.results);

                    this._cache.AddOrReplace(ticker, stockPrices);

                    return new Either<IEnumerable<StockPrice>, Exception>(stockPrices);
                },
                exception =>
                {
                    _logger.LogError(exception, "Error fetching data from the polygon.io API");
                    return new Either<IEnumerable<StockPrice>, Exception>(exception);
                });
        }
    }
}
