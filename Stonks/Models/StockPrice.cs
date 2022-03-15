namespace Stonks.Models
{
    public class StockPrice
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public double TradingVolume { get; set; }

        public double VolumeWeightedAveragePrice { get; set; }

        public double OpenPrice { get; set; }

        public double ClosePrice { get; set; }

        public double HighestPrice { get; set; }

        public double LowestPrice { get; set; }

        public int NumberOfTransactions { get; set; }

        public string DateTime { get; set; }
    }
}
