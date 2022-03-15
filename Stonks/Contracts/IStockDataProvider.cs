using Stonks.Models;

namespace Stonks.Contracts
{
    public interface IStockDataProvider
    {
        Task<Either<AggregateBars, Exception>> GetAggregateDataByTicker(string ticker);
    }
}
