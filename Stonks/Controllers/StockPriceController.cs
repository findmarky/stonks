using Microsoft.AspNetCore.Mvc;
using Stonks.Contracts;
using Stonks.Exceptions;
using Stonks.Models;
using Stonks.Services;

namespace Stonks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockPriceController : ControllerBase
    {
        private readonly IStockPriceService _stockPriceService;

        public StockPriceController(IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockPrice>>> Get(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                return BadRequest("A ticker is required.");
            }

            var stockPricesOrException = await this._stockPriceService.GetStockPricesByTicker(ticker);

            return stockPricesOrException.Match<ActionResult>((stockPrices) =>
            {
                return Ok(stockPrices);
            },
            exception =>
            {
                return exception switch
                {
                    StockNotFoundException => NotFound(),
                    _ => Problem()
                };
            });
        }
    }
}