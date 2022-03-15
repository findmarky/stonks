using Stonks.Models;

namespace Stonks.Contracts
{
    public interface IStockPriceService
    {
        Task<Either<IEnumerable<StockPrice>, Exception>> GetStockPricesByTicker(string ticker);
    }
}
