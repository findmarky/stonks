using RestSharp;
using Stonks.Configuration;
using Stonks.Contracts;
using Stonks.Exceptions;
using Stonks.Models;
using Stonks.Utility;

namespace Stonks.Services
{
    public class PolygonService : IStockDataProvider
    {
        private readonly PolygonConfiguration _polygonConfiguration;

        public PolygonService(PolygonConfiguration polygonConfiguration)
        {
            _polygonConfiguration = polygonConfiguration;
        }

        public async Task<Either<AggregateBars, Exception>> GetAggregateDataByTicker(string ticker)
        {
            try
            {
                var client = new RestClient(_polygonConfiguration.BaseUrl).AddDefaultHeader("Authorization", $"Bearer {_polygonConfiguration.ApiKey}");
                var queryString = CreateQueryString(ticker);
                var request = new RestRequest(queryString);

                var aggregateBarResult = await client.GetAsync<AggregateBars>(request);

                if (aggregateBarResult?.results == null)
                {
                    return Either<AggregateBars, Exception>.Create(new StockNotFoundException($"No aggregated data returned for ticket {ticker}"));
                }

                aggregateBarResult.results.ForEach(result =>
                {
                    result.d = DateUtility.ConvertUnixMSecTimestampToDate(result.t, "yyyy-MM-dd");
                });

                return Either<AggregateBars, Exception>.Create(aggregateBarResult);
            }
            catch (Exception exception)
            {
                return Either<AggregateBars, Exception>.Create(exception);
            }
        }

        private static string CreateQueryString(string ticker)
        {
            var from = DateTime.Now;
            var to = from.AddMonths(-1);

            return $"aggs/ticker/{ticker}/range/1/day/{to:yyyy-MM-dd}/{from:yyyy-MM-dd}?adjusted=true&sort=asc&limit=120";
        }
    }
}
